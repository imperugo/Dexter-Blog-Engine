#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ConfigurationDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Services
{
	using Common.Logging;

	using Dexter.Data.Raven.Session;
	using Dexter.Entities;

	using global::Raven.Client;

	public class ConfigurationDataService : ServiceBase, IConfigurationDataService
	{
		#region Constructors and Destructors

		public ConfigurationDataService(ILog logger, ISessionFactory sessionFactory)
			: base(logger, sessionFactory)
		{
		}

		#endregion

		#region Public Methods and Operators

		public BlogConfigurationDto GetConfiguration()
		{
			BlogConfigurationDto configuration = this.Session.Load<BlogConfigurationDto>(1);

			if (configuration == null)
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