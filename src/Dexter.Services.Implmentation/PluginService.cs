#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PluginService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2013/01/07
// Last edit:	2013/01/07
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Implmentation
{
	using System;

	using AutoMapper;

	using Dexter.Data;
	using Dexter.Data.Exceptions;
	using Dexter.Entities;
	using Dexter.Entities.Result;

	public class PluginService : IPluginService
	{
		#region Fields

		private readonly IPackageInstaller packageInstaller;

		private readonly IPluginDataService pluginDataService;

		#endregion

		#region Constructors and Destructors

		public PluginService(IPluginDataService pluginDataService, IPackageInstaller packageInstaller)
		{
			this.pluginDataService = pluginDataService;
			this.packageInstaller = packageInstaller;
		}

		#endregion

		#region Public Methods and Operators

		public void DisablePlugin(string pluginId, Version version)
		{
			var plugin = this.pluginDataService.GetPlugin(pluginId, version);

			if (plugin == null)
			{
				throw new DexterException("Unable to find the specified plugin");
			}

			this.pluginDataService.DisablePlugin(plugin);
		}

		public void EnablePlugin(string pluginId, Version version)
		{
			var plugin = this.pluginDataService.GetPlugin(pluginId, version);

			if (plugin == null)
			{
				throw new DexterException("Unable to find the specified plugin");
			}

			this.pluginDataService.EnablePlugin(plugin);
		}

		public IPagedResult<PluginDto> GetInstalledPlugin(int pageIndex, int pageSize)
		{
			return this.pluginDataService.GetInstalledPlugin(pageIndex, pageSize);
		}

		public void Install(string pluginId, Version version)
		{
			var package = this.packageInstaller.Install(pluginId, version);

			this.pluginDataService.EnablePlugin(package.MapTo<PluginDto>());
		}

		public void Uninstall(string pluginId, Version version)
		{
			this.DisablePlugin(pluginId, version);

			this.packageInstaller.Uninstall(pluginId, version);
		}

		public void Update(string pluginId, Version version)
		{
			this.packageInstaller.Update(pluginId, version);
		}

		#endregion
	}
}