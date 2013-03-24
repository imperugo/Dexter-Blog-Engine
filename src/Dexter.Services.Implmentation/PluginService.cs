#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PluginService.cs
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

namespace Dexter.Services.Implmentation
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Reflection;

	using AutoMapper;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Dependency;
	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.Services.Plugins;
	using Dexter.Shared.Exceptions;

	public class PluginService : IPluginService
	{
		#region Fields

		private readonly IPackageInstaller packageInstaller;

		private readonly IPluginDataService pluginDataService;

		private readonly ILog logger;

		private readonly IDexterContainer container;

		#endregion

		#region Constructors and Destructors

		public PluginService(IPluginDataService pluginDataService, IPackageInstaller packageInstaller, ILog logger, IDexterContainer container)
		{
			this.pluginDataService = pluginDataService;
			this.packageInstaller = packageInstaller;
			this.logger = logger;
			this.container = container;
		}

		#endregion

		#region Public Methods and Operators

		public void LoadAllEnabledPlugins()
		{
			IPagedResult<PluginDto> plugins = this.GetInstalledPlugin(1, 100);

			foreach (var plugin in plugins.Result)
			{
				this.EnablePlugin(plugin.Id, plugin.Version);
			}
		}

		public void UnloadAllEnabledPlugins()
		{
			IPagedResult<PluginDto> plugins = this.GetInstalledPlugin(1, 100);

			foreach (var plugin in plugins.Result)
			{
				this.DisablePlugin(plugin.Id, plugin.Version);
			}

			throw new DexterRestartRequiredException(this.GetType());
		}

		public void DisablePlugin(string pluginId, Version version)
		{
			PluginDto plugin = this.pluginDataService.GetPlugin(pluginId, version);

			if (plugin == null)
			{
				throw new DexterPluginException("Unable to find the specified plugin", pluginId);
			}

			this.pluginDataService.DisablePlugin(plugin);
		}

		public void EnablePlugin(string pluginId, Version version)
		{
			PluginDto plugin = this.pluginDataService.GetPlugin(pluginId, version);

			if (plugin == null)
			{
				throw new DexterException("Unable to find the specified plugin");
			}

			string pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\Extensions\Plugins", plugin.Title, @"\Assemblies");

			if (!Directory.Exists(pluginPath))
			{
				throw new DirectoryNotFoundException(string.Format("Unable to find the specified path '{0}'.", pluginPath));
			}

			string[] files = Directory.GetFiles(pluginPath, "*.dll", SearchOption.TopDirectoryOnly);

			if (files.Length < 1)
			{
				this.logger.WarnFormat("Unable to find the assemblies for the plugin '{0}'", plugin.Title);
				return;
			}

			foreach (var file in files)
			{
				var pluginAssemblies = Assembly.LoadFrom(file);

				Type pluginType = pluginAssemblies.GetTypes().FirstOrDefault(t => !t.IsInterface && !t.IsAbstract && typeof(IPlugin).IsAssignableFrom(t));

				if (pluginType == null)
				{
					logger.DebugFormat("The is not an IPlugin for the assemly '{0}' for the plugin '{1}'.", Path.GetFileName(file), plugin.Title);
					continue;
				}

				container.Register(pluginType, pluginType, LifeCycle.Singleton);
				var pluginInstance = (IPlugin)DexterContainer.Resolve(pluginType);

				if (!plugin.IsInstalled)
				{
					logger.DebugFormat("Calling Setup method for '{0}' for the plugin '{1}'.", pluginType, plugin.Title);
					pluginInstance.Setup();
					plugin.IsInstalled = true;
				}

				logger.DebugFormat("Calling Initialize method for '{0}' for the plugin '{1}'.", pluginType, plugin.Title);
				pluginInstance.Initialize();
				plugin.Enabled = true;
			}

			this.pluginDataService.UpdatePlugin(plugin);
		}

		public IPagedResult<PluginDto> GetInstalledPlugin(int pageIndex, int pageSize)
		{
			return this.pluginDataService.GetInstalledPlugin(pageIndex, pageSize);
		}

		public void Install(string pluginId, Version version)
		{
			PackageDto package = this.packageInstaller.Install(pluginId, version);

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