#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ConfigurationDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/12/23
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using global::AutoMapper;

	using Common.Logging;

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Session;
	using Dexter.Entities;

	using global::Raven.Abstractions.Indexing;
	using global::Raven.Client;
	using global::Raven.Client.Document;
	using global::Raven.Client.Indexes;

	public class ConfigurationDataService : ServiceBase, IConfigurationDataService
	{
		private IDocumentStore store;

		#region Constructors and Destructors

		public ConfigurationDataService(ILog logger, ISessionFactory sessionFactory, IDocumentStore store)
			: base(logger, sessionFactory)
		{
			this.store = store;
		}

		#endregion

		#region Public Methods and Operators

		public BlogConfigurationDto GetConfiguration()
		{
			var configuration = this.Session.Load<BlogConfigurationDto>(1);

			if (configuration == null )
			{
				return null;
			}

			return configuration;
		}

		public void SaveConfiguration(BlogConfigurationDto configurationDto)
		{
			this.Session.Store(configurationDto);
		}

		#endregion
	}
}