#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPackageInstaller.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/07
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services
{
	using System;

	using Dexter.Shared.Dto;
	using Dexter.Shared.Result;

	/// <summary>
	/// The contract for the Package installer
	/// </summary>
	public interface IPackageInstaller
	{
		#region Public Events

		/// <summary>
		/// This event will raise after the package Installation. The event is raised by by the implementation of <see cref="Install(PackageDto)"/> or <see cref="Install(string,Version)"/>.
		/// </summary>
		event EventHandler<PackageEventArgs> PackageInstalled;

		/// <summary>
		/// This event will raise before to install a<see cref="PackageDto"/>. The event is raised by by the implementation of <see cref="Install(PackageDto)"/> or <see cref="Install(string,Version)"/>.
		/// </summary>
		event EventHandler<PackageEventArgs> PackageInstalling;

		/// <summary>
		/// This event will raise after the package uninstall. The event is raised by by the implementation of <see cref="Uninstall(PackageDto)"/> or <see cref="Uninstall(string,Version)"/>.
		/// </summary>
		event EventHandler<PackageEventArgs> PackageUnistalled;

		/// <summary>
		/// This event will raise before to uninstall a<see cref="PackageDto"/>. The event is raised by by the implementation of <see cref="Uninstall(PackageDto)"/> or <see cref="Uninstall(string,Version)"/>.
		/// </summary>
		event EventHandler<PackageEventArgs> PackageUnistalling;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Gets the specified package id.
		/// </summary>
		/// <param name="packageId">The package id.</param>
		/// <param name="version">The version.</param>
		/// <returns></returns>
		PackageDto Get(string packageId, Version version);

		/// <summary>
		/// Installs the package with the specified <param name="packageId" /> and <param name="version" />
		/// </summary>
		/// <param name="packageId">The package id.</param>
		/// <param name="version">The version.</param>
		PackageDto Install(string packageId, Version version);

		/// <summary>
		/// Searches the installed plugin.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <returns></returns>
		IPagedResult<PackageDto> SearchInstalledPlugin(PackageSearchFilter filter);

		/// <summary>
		/// Searches the installed themes.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <returns></returns>
		IPagedResult<PackageDto> SearchInstalledThemes(PackageSearchFilter filter);

		/// <summary>
		/// Searches the plugin.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <returns></returns>
		IPagedResult<PackageDto> SearchPlugin(PackageSearchFilter filter);

		/// <summary>
		/// Searches the themes.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <returns></returns>
		IPagedResult<PackageDto> SearchThemes(PackageSearchFilter filter);

		/// <summary>
		/// Uninstalls the package with the specified <param name="packageId" /> and <param name="version" />
		/// </summary>
		/// <param name="packageId">The package id.</param>
		/// <param name="version">The version.</param>
		PackageDto Uninstall(string packageId, Version version);

		/// <summary>
		/// Updates the package with the specified <param name="packageId" /> and <param name="version" />
		/// </summary>
		/// <param name="packageId">The package id.</param>
		/// <param name="version">The version.</param>
		PackageDto Update(string packageId, Version version);

		/// <summary>
		/// Updates the available.
		/// </summary>
		/// <param name="packageId">The package id.</param>
		/// <param name="version">The version.</param>
		/// <returns></returns>
		bool UpdateAvailable(string packageId, Version version);

		#endregion
	}

	/// <summary>
	/// The container for the packaged search filter
	/// </summary>
	public class PackageSearchFilter
	{
		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PackageSearchFilter" /> class.
		/// </summary>
		public PackageSearchFilter()
		{
			this.PageSize = 10;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the index of the page.
		/// </summary>
		/// <value>
		/// The index of the page.
		/// </value>
		public int PageIndex { get; set; }

		/// <summary>
		/// Gets or sets the size of the page.
		/// </summary>
		/// <value>
		/// The size of the page.
		/// </value>
		public int PageSize { get; set; }

		/// <summary>
		/// Gets or sets the tags.
		/// </summary>
		/// <value>
		/// The tags.
		/// </value>
		public string[] Tags { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		#endregion
	}

	public class PackageEventArgs : EventArgs
	{
		#region Constructors and Destructors

		public PackageEventArgs(string installPath, PackageDto package)
		{
			this.InstallPath = installPath;
			this.Package = package;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the install path.
		/// </summary>
		/// <value>
		/// The install path.
		/// </value>
		public string InstallPath { get; private set; }

		/// <summary>
		/// Gets the package.
		/// </summary>
		/// <value>
		/// The package.
		/// </value>
		public PackageDto Package { get; private set; }

		#endregion
	}
}