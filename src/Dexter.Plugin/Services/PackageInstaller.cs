#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PackageInstaller.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2013/01/06
// Last edit:	2013/01/06
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Plugin.Services
{
	using System;

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
			//TODO:using nuget standard feed (it's a spike)
			this.packageRepository = PackageRepositoryFactory.Default.CreateRepository("https://nuget.org/api/v2/");

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
			if (packageId == null)
			{
				throw new ArgumentNullException("packageId", "The package Id cannot be null");
			}

			if (packageId == string.Empty)
			{
				throw new ArgumentException("The package Id must contain a valid value.", "packageId");
			}

			Version packageVersion = String.IsNullOrEmpty(version) ? null : new Version(version);
			IPackage pkg = this.packageRepository.FindPackage(packageId, new SemanticVersion(packageVersion));

			if (pkg == null)
			{
				throw new ArgumentException(string.Format("The specified package could not be found, id:{0} version:{1}", packageId, packageVersion));
			}

			this.Install(pkg);
		}

		public void Uninstall(string packageId, string version)
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

		#endregion
	}
}