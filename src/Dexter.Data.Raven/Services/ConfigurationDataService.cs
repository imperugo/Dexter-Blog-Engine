#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ConfigurationDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/02
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Services
{
	using System.Linq;
	using System.Security;

	using Common.Logging;

	using Dexter.Data.Raven.Session;
	using Dexter.Shared.Dto;

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

		public void CreateSetupConfiguration(BlogConfigurationDto configurationDto)
		{
			if (this.GetConfiguration() != null)
			{
				throw new SecurityException("This method could be called only during the setup procedure");
			}

			this.SaveConfiguration(configurationDto);
		}

		public void SaveConfiguration(BlogConfigurationDto configurationDto)
		{
			this.Session.Store(configurationDto);
		}

		#endregion
	}
}