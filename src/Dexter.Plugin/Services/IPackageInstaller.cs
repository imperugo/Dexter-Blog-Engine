#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPackageInstaller.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2013/01/03
// Last edit:	2013/01/07
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Plugin.Services
{
	using Dexter.Entities.Result;

	using NuGet;

	public interface IPackageInstaller
	{
		#region Public Methods and Operators

		void Install(IPackage package);

		void Install(string packageId, string version);

		IPagedResult<IPackage> SearchInstalledPlugin(PackageSearchFilter filter);

		IPagedResult<IPackage> SearchInstalledThemes(PackageSearchFilter filter);

		IPagedResult<IPackage> SearchPlugin(PackageSearchFilter filter);

		IPagedResult<IPackage> SearchThemes(PackageSearchFilter filter);

		void Uninstall(string packageId, string version);

		void Uninstall(IPackage package);

		void Update(string packageId, string version);

		void Update(IPackage package);

		#endregion
	}
}