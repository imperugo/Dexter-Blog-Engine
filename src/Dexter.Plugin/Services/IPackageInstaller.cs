#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPackageInstaller.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2013/01/03
// Last edit:	2013/01/03
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
	using Dexter.Plugin.Model;

	using NuGet;

	public interface IPackageInstaller
	{
		#region Public Methods and Operators

		PackageInfo Install(IPackage package, string location, string applicationPath);

		PackageInfo Install(string packageId, string version, string location, string applicationFolder);

		void Uninstall(string packageId, string applicationFolder);

		#endregion
	}

	public class PackageInstaller : IPackageInstaller
	{
		#region Public Methods and Operators

		public PackageInfo Install(IPackage package, string location, string applicationPath)
		{
			IPackageRepository packageRepository = PackageRepositoryFactory.Default.CreateRepository(location);
			
			return InstallPackage(package, packageRepository, location, applicationPath);
		}

		public PackageInfo Install(string packageId, string version, string location, string applicationFolder)
		{
			IPackageRepository packageRepository = PackageRepositoryFactory.Default.CreateRepository(location);

			Version packageVersion = String.IsNullOrEmpty(version) ? null : new Version(version);
			IPackage pkg = packageRepository.FindPackage(packageId, new SemanticVersion(packageVersion));

			if (pkg == null)
			{
				throw new ArgumentException(string.Format("The specified package could not be found, id:{0} version:{1}", packageId, packageVersion));
			}

			return this.InstallPackage(pkg, packageRepository, location, pkg.GetPluginFolder().FullName);
		}

		public void Uninstall(string packageId, string applicationFolder)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Methods

		protected PackageInfo InstallPackage(IPackage package, IPackageRepository packageRepository, string location, string applicationPath)
		{
			PackageManager packageManager = new PackageManager(
				packageRepository, 
				new DefaultPackagePathResolver(location), 
				new PhysicalFileSystem(package.GetPluginFolder().FullName))
				                                {
					                                Logger = new NugetLogger()
				                                };

			packageManager.InstallPackage(package, true, false);

			return new PackageInfo
				       {
					       ExtensionName = package.Title ?? package.Id, 
					       ExtensionVersion = package.Version.ToString(), 
					       ExtensionType = package.IsTheme() ? "Theme" : "Plugin", 
					       ExtensionPath = package.GetPluginFolder().FullName
				       };
		}

		#endregion

		//private bool BackupExtensionFolder(IPackage package)
		//{
		//	DirectoryInfo source = package.GetPluginFolder();

		//	if (source.Exists)
		//	{
		//		var tempPath = _virtualPathProvider.Combine("~", extensionFolder, "_Backup", extensionId);
		//		string localTempPath = null;
		//		for (int i = 0; i < 1000; i++)
		//		{
		//			localTempPath = _virtualPathProvider.MapPath(tempPath) + (i == 0 ? "" : "." + i.ToString());
		//			if (!Directory.Exists(localTempPath))
		//			{
		//				Directory.CreateDirectory(localTempPath);
		//				break;
		//			}
		//			localTempPath = null;
		//		}

		//		if (localTempPath == null)
		//		{
		//			throw new OrchardException(T("Backup folder {0} has too many backups subfolder (limit is 1,000)", tempPath));
		//		}

		//		var backupFolder = new DirectoryInfo(localTempPath);
		//		_folderUpdater.Backup(source, backupFolder);
		//		_notifier.Information(T("Successfully backed up local package to local folder \"{0}\"", backupFolder));

		//		return true;
		//	}

		//	return false;
		//}
	}
}