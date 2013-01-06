#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PackageInstaller.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2013/01/06
// Last edit:	2013/01/07
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Plugin.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Dexter.Entities.Result;
	using Dexter.Plugin.Extensions;
	using Dexter.Plugin.Logger;

	using NuGet;

	using PackageExtensions = Dexter.Plugin.Extensions.PackageExtensions;

	public class PackageInstaller : IPackageInstaller
	{
		#region Fields

		private readonly IPackageRepository packageRepository;

		private readonly PackageManager pluginPackageManager;

		private readonly PackageManager themePackageManager;

		#endregion

		#region Constructors and Destructors

		public PackageInstaller()
		{
			//TODO:using nuget standard feed (it's a spike). In the future the urls must come from the configuration
			this.packageRepository = new AggregateRepository(new[]
				                                                 {
					                                                 PackageRepositoryFactory.Default.CreateRepository("https://nuget.org/api/v2/")
				                                                 });

			this.themePackageManager = new PackageManager(this.packageRepository, PackageExtensions.ThemesFolder.FullName)
				                           {
					                           Logger = new NugetLogger()
				                           };

			this.pluginPackageManager = new PackageManager(this.packageRepository, PackageExtensions.PluginFolder.FullName)
				                            {
					                            Logger = new NugetLogger()
				                            };
		}

		#endregion

		#region Public Methods and Operators

		public void Install(IPackage package)
		{
			if (package == null)
			{
				throw new ArgumentNullException("package", "The package cannot be null.");
			}

			if (package.IsTheme())
			{
				this.themePackageManager.InstallPackage(package, true, false);
			}
			else
			{
				this.pluginPackageManager.InstallPackage(package, true, false);
			}
		}

		public void Install(string packageId, string version)
		{
			IPackage package = this.FindPackage(packageId, version);

			this.Install(package);
		}

		public IPagedResult<IPackage> SearchInstalledPlugin(PackageSearchFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter", "The package search filter cannot be null.");
			}

			IQueryable<IPackage> query = this.pluginPackageManager.LocalRepository.GetPackages();
			IQueryable<IPackage> queryCount = this.pluginPackageManager.LocalRepository.GetPackages();

			this.ApplyFilter(query, filter);
			this.ApplyFilter(queryCount, filter);

			List<IPackage> result = query.OrderBy(p => p.Id)
										 .Skip(filter.PageIndex)
										 .Take(filter.PageSize)
										 .ToList();
			return new PagedResult<IPackage>(filter.PageIndex, filter.PageSize, result, queryCount.Count());
		}

		public IPagedResult<IPackage> SearchPlugin(PackageSearchFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter", "The package search filter cannot be null.");
			}

			IQueryable<IPackage> query = this.pluginPackageManager.SourceRepository.GetPackages();
			IQueryable<IPackage> queryCount = this.pluginPackageManager.SourceRepository.GetPackages();

			this.ApplyFilter(query, filter);
			this.ApplyFilter(queryCount, filter);

			List<IPackage> result = query.OrderBy(p => p.Id)
			                             .Skip(filter.PageIndex)
			                             .Take(filter.PageSize)
			                             .ToList();

			return new PagedResult<IPackage>(filter.PageIndex, filter.PageSize, result, queryCount.Count());
		}

		public IPagedResult<IPackage> SearchInstalledThemes(PackageSearchFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter", "The package search filter cannot be null.");
			}

			IQueryable<IPackage> query = this.themePackageManager.LocalRepository.GetPackages();
			IQueryable<IPackage> queryCount = this.themePackageManager.LocalRepository.GetPackages();

			this.ApplyFilter(query, filter);
			this.ApplyFilter(queryCount, filter);

			List<IPackage> result = query.OrderBy(p => p.Id)
										 .Skip(filter.PageIndex)
										 .Take(filter.PageSize)
										 .ToList();

			return new PagedResult<IPackage>(filter.PageIndex, filter.PageSize, result, queryCount.Count());
		}

		public IPagedResult<IPackage> SearchThemes(PackageSearchFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter", "The package search filter cannot be null.");
			}

			IQueryable<IPackage> query = this.themePackageManager.SourceRepository.GetPackages();
			IQueryable<IPackage> queryCount = this.themePackageManager.SourceRepository.GetPackages();

			this.ApplyFilter(query, filter);
			this.ApplyFilter(queryCount, filter);

			List<IPackage> result = query.OrderBy(p => p.Id)
			                             .Skip(filter.PageIndex)
			                             .Take(filter.PageSize)
			                             .ToList();

			return new PagedResult<IPackage>(filter.PageIndex, filter.PageSize, result, queryCount.Count());
		}

		public void Uninstall(string packageId, string version)
		{
			IPackage package = this.FindPackage(packageId, version);

			this.Uninstall(package);
		}

		public void Uninstall(IPackage package)
		{
			if (package == null)
			{
				throw new ArgumentNullException("package", "The package cannot be null.");
			}

			if (package.IsTheme())
			{
				this.themePackageManager.UninstallPackage(package);
			}
			else
			{
				this.pluginPackageManager.UninstallPackage(package);
			}
		}

		public void Update(string packageId, string version)
		{
			IPackage package = this.FindPackage(packageId, version);

			this.Update(package);
		}

		public void Update(IPackage package)
		{
			if (package.IsTheme())
			{
				this.themePackageManager.UpdatePackage(package, false, true);
			}
			else
			{
				this.pluginPackageManager.UpdatePackage(package, false, true);
			}
		}

		#endregion

		#region Methods

		private void ApplyFilter(IQueryable<IPackage> query, PackageSearchFilter filter)
		{
			if (!string.IsNullOrEmpty(filter.Title))
			{
				query = query.Where(x => x.Title == filter.Title);
			}

			if (filter.Tags != null && filter.Tags.Any())
			{
				filter.Tags.Aggregate(query, (current, tag) => current.Where(p => p.Tags.Contains(tag)));
			}
		}

		private IPackage FindPackage(string packageId, string version)
		{
			if (packageId == null)
			{
				throw new ArgumentNullException("packageId", "The package Id cannot be null");
			}

			if (packageId == string.Empty)
			{
				throw new ArgumentException("The package Id must contain a valid value.", "packageId");
			}

			Version packageVersion = String.IsNullOrEmpty(version) ? null : new Version(version);

			IPackage package = packageId.IsTheme()
				                   ? this.themePackageManager.LocalRepository.FindPackage(packageId, new SemanticVersion(packageVersion))
				                   : this.pluginPackageManager.LocalRepository.FindPackage(packageId, new SemanticVersion(packageVersion));

			if (package == null)
			{
				throw new ArgumentException(string.Format("The specified package could not be found, id:{0} version:{1}", packageId, version));
			}

			return package;
		}

		#endregion
	}

	public class PackageSearchFilter
	{
		#region Constructors and Destructors

		public PackageSearchFilter()
		{
			this.PageSize = 10;
		}

		#endregion

		#region Public Properties

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		public string[] Tags { get; set; }

		public string Title { get; set; }

		#endregion
	}
}