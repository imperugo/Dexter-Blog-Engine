#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPluginService.cs
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

	using Dexter.Entities;
	using Dexter.Entities.Result;

	public interface IPluginService
	{
		#region Public Methods and Operators

		/// <summary>
		/// Disables the plugin.
		/// </summary>
		/// <param name="pluginId">The plugin id.</param>
		/// <param name="version">The version.</param>
		void DisablePlugin(string pluginId, Version version);

		/// <summary>
		/// Enables the plugin.
		/// </summary>
		/// <param name="pluginId">The plugin id.</param>
		/// <param name="version">The version.</param>
		void EnablePlugin(string pluginId, Version version);

		/// <summary>
		/// Gets the installed plugin.
		/// </summary>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		IPagedResult<PluginDto> GetInstalledPlugin(int pageIndex, int pageSize);

		/// <summary>
		/// Installs the plugin with the specified <param name="pluginId" /> and <param name="version" />
		/// </summary>
		/// <param name="pluginId">The plugin id.</param>
		/// <param name="version">The version.</param>
		void Install(string pluginId, Version version);

		/// <summary>
		/// Uninstalls the plugin with the specified <param name="pluginId" /> and <param name="version" />
		/// </summary>
		/// <param name="pluginId">The package id.</param>
		/// <param name="version">The version.</param>
		void Uninstall(string pluginId, Version version);

		/// <summary>
		/// Updates the plugin with the specified <param name="pluginId" /> and <param name="version" />
		/// </summary>
		/// <param name="pluginId">The package id.</param>
		/// <param name="version">The version.</param>
		void Update(string pluginId, Version version);

		#endregion
	}
}