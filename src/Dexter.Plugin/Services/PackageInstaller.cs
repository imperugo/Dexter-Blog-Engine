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

namespace Dexter.PackageInstaller.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using AutoMapper;

	using Dexter.Data.Exceptions;
	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.PackageInstaller.Extensions;
	using Dexter.PackageInstaller.Logger;
	using Dexter.Services;

	using NuGet;

	using PackageExtensions = Dexter.PackageInstaller.Extensions.PackageExtensions;

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
			// TODO:using nuget standard feed (it's a spike). In the future the urls must come from the configuration
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

			this.pluginPackageManager.PackageInstalling += this.PluginPackageManager_PackageInstalling;
			this.pluginPackageManager.PackageInstalled += this.PluginPackageManager_PackageInstalled;
			this.pluginPackageManager.PackageUninstalling += this.PluginPackageManager_PackageUninstalling;
			this.pluginPackageManager.PackageUninstalled += this.PluginPackageManager_PackageUninstalled;
		}

		#endregion

		#region Public Events

		public event EventHandler<PackageEventArgs> PackageInstalled;

		public event EventHandler<PackageEventArgs> PackageInstalling;

		public event EventHandler<PackageEventArgs> PackageUnistalled;

		public event EventHandler<PackageEventArgs> PackageUnistalling;

		#endregion

		#region Public Methods and Operators

		public PackageDto Install(string packageId, Version version)
		{
			IPackage package = this.FindPackage(packageId, version);

			this.Install(package);

			return package.MapTo<PackageDto>();
		}

		public IPagedResult<PackageDto> SearchInstalledPlugin(PackageSearchFilter filter)
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
			return new PagedResult<PackageDto>(filter.PageIndex, filter.PageSize, result.MapTo<PackageDto>(), queryCount.Count());
		}

		public IPagedResult<PackageDto> SearchInstalledThemes(PackageSearchFilter filter)
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

			return new PagedResult<PackageDto>(filter.PageIndex, filter.PageSize, result.MapTo<PackageDto>(), queryCount.Count());
		}

		public IPagedResult<PackageDto> SearchPlugin(PackageSearchFilter filter)
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

			return new PagedResult<PackageDto>(filter.PageIndex, filter.PageSize, result.MapTo<PackageDto>(), queryCount.Count());
		}

		public IPagedResult<PackageDto> SearchThemes(PackageSearchFilter filter)
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

			return new PagedResult<PackageDto>(filter.PageIndex, filter.PageSize, result.MapTo<PackageDto>(), queryCount.Count());
		}

		public PackageDto Uninstall(string packageId, Version version)
		{
			IPackage package = this.FindInstalledPackage(packageId, version);

			this.Uninstall(package);

			return package.MapTo<PackageDto>();
		}

		public PackageDto Update(string packageId, Version version)
		{
			IPackage package = this.FindPackage(packageId, version);

			this.Update(package);

			return package.MapTo<PackageDto>();
		}

		public bool UpdateAvailable(string packageId, Version version)
		{
			IPackage package = this.FindPackage(packageId, version);

			if (package == null)
			{
				throw new DexterException("Unable to locate the specified package");
			}

			return !package.IsLatestVersion;
		}

		public PackageDto Get(string packageId, Version version)
		{
			throw new NotImplementedException();
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

		private IPackage FindPackage(string packageId, Version version)
		{
			if (packageId == null)
			{
				throw new ArgumentNullException("packageId", "The package Id cannot be null");
			}

			if (packageId == string.Empty)
			{
				throw new ArgumentException("The package Id must contain a valid value.", "packageId");
			}

			IPackage package = packageId.IsTheme()
								   ? this.themePackageManager.SourceRepository.FindPackage(packageId, new SemanticVersion(version))
								   : this.pluginPackageManager.SourceRepository.FindPackage(packageId, new SemanticVersion(version));

			if (package == null)
			{
				throw new ArgumentException(string.Format("The specified package could not be found, id:{0} version:{1}", packageId, version));
			}

			return package;
		}

		private IPackage FindInstalledPackage(string packageId, Version version)
		{
			if (packageId == null)
			{
				throw new ArgumentNullException("packageId", "The package Id cannot be null");
			}

			if (packageId == string.Empty)
			{
				throw new ArgumentException("The package Id must contain a valid value.", "packageId");
			}

			IPackage package = packageId.IsTheme()
				                   ? this.themePackageManager.LocalRepository.FindPackage(packageId, new SemanticVersion(version))
				                   : this.pluginPackageManager.LocalRepository.FindPackage(packageId, new SemanticVersion(version));

			if (package == null)
			{
				throw new ArgumentException(string.Format("The specified package could not be found, id:{0} version:{1}", packageId, version));
			}

			return package;
		}

		private void Install(IPackage package)
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

		private void PluginPackageManager_PackageInstalled(object sender, PackageOperationEventArgs e)
		{
			PackageDto package = e.Package.MapTo<PackageDto>();
			string packagePath = e.InstallPath;

			this.PackageInstalled.Raise(sender, new PackageEventArgs(packagePath, package));
		}

		private void PluginPackageManager_PackageInstalling(object sender, PackageOperationEventArgs e)
		{
			PackageDto package = e.Package.MapTo<PackageDto>();
			string packagePath = e.InstallPath;

			this.PackageInstalling.Raise(sender, new PackageEventArgs(packagePath, package));
		}

		private void PluginPackageManager_PackageUninstalled(object sender, PackageOperationEventArgs e)
		{
			PackageDto package = e.Package.MapTo<PackageDto>();
			string packagePath = e.InstallPath;

			this.PackageUnistalled.Raise(sender, new PackageEventArgs(packagePath, package));
		}

		private void PluginPackageManager_PackageUninstalling(object sender, PackageOperationEventArgs e)
		{
			PackageDto package = e.Package.MapTo<PackageDto>();
			string packagePath = e.InstallPath;

			this.PackageUnistalling.Raise(sender, new PackageEventArgs(packagePath, package));
		}

		private void Uninstall(IPackage package)
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

		private void Update(IPackage package)
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
	}
}