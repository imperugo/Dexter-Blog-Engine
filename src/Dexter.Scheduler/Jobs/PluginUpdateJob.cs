#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PluginUpdateJob.cs
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

namespace Dexter.Scheduler.Jobs
{
	using AutoMapper;

	using Dexter.Data;
	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.Services;

	using global::Quartz;

	public class PluginUpdateJob : IJob
	{
		#region Fields

		private readonly IPackageInstaller packageInstaller;

		private readonly IPluginDataService pluginService;

		#endregion

		#region Constructors and Destructors

		public PluginUpdateJob(IPluginDataService pluginService, IPackageInstaller packageInstaller)
		{
			this.pluginService = pluginService;
			this.packageInstaller = packageInstaller;
		}

		#endregion

		#region Public Methods and Operators

		public void Execute(IJobExecutionContext context)
		{
			IPagedResult<PluginDto> plugins = this.pluginService.GetInstalledPlugin(1, 1000);

			foreach (PluginDto plugin in plugins.Result)
			{
				PackageDto package = this.packageInstaller.Get(plugin.PackageId, plugin.Version);

				PluginDto p = package.MapPropertiesToInstance(plugin);

				this.pluginService.UpdatePlugin(p);
			}
		}

		#endregion
	}
}