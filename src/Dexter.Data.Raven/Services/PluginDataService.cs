#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PluginDataService.cs
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

namespace Dexter.Data.Raven.Services
{
	using System;
	using System.Linq;


	using Common.Logging;

	using Dexter.Data.Exceptions;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Extensions;
	using Dexter.Data.Raven.Session;
	using Dexter.Entities;
	using Dexter.Entities.Result;

	using global::Raven.Client;

	public class PluginDataService : ServiceBase, IPluginDataService
	{
		#region Constructors and Destructors

		public PluginDataService(ILog logger, ISessionFactory sessionFactory)
			: base(logger, sessionFactory)
		{
		}

		#endregion

		#region Public Methods and Operators

		public IPagedResult<PluginDto> GetInstalledPlugin(int pageIndex, int pageSize)
		{
			if (pageIndex < 1)
			{
				throw new ArgumentException("The page index must be greater than 0", "pageIndex");
			}

			if (pageSize < 1)
			{
				throw new ArgumentException("The page size must be greater than 0", "pageSize");
			}

			RavenQueryStatistics stats;

			return this.Session.Query<Plugin>()
			           .Statistics(out stats)
			           .OrderByDescending(plugin => plugin.Id)
			           .ToPagedResult<Plugin, PluginDto>(pageIndex, pageSize, stats);
		}

		public PluginDto GetPlugin(string packageId, Version version)
		{
			throw new NotImplementedException();
		}

		public void UpdatePlugin(PluginDto item)
		{
			throw new NotImplementedException();
		}

		public void DisablePlugin(PluginDto item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", "The package cannot be null");
			}

			var plugin = this.Session.Load<Plugin>(item.Id);

			if (plugin == null)
			{
				throw new DexterException("Unable to find the specified package");
			}

			plugin.Enabled = false;

			this.Session.Store(plugin);
		}

		public void EnablePlugin(PluginDto item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", "The package cannot be null");
			}

			var plugin = this.Session.Load<Plugin>(item.Id);

			if (plugin == null)
			{
				throw new DexterException("Unable to find the specified package");
			}

			plugin.Enabled = true;

			this.Session.Store(plugin);
		}

		#endregion
	}
}