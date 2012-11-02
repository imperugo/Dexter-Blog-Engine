#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ConfigurationService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services.Implmentation
{
	using System;
	using System.Threading.Tasks;

	using Dexter.Caching;
	using Dexter.Data;
	using Dexter.Entities;

	public class ConfigurationService : IConfigurationService
	{
		#region Fields

		private readonly IConfigurationDataService configurationDataService;

		private readonly ICacheProvider cacheProvider;

		#endregion

		#region Constructors and Destructors

		public ConfigurationService(ICacheProvider cacheProvider, IConfigurationDataService configurationDataService)
		{
			this.cacheProvider = cacheProvider;
			this.configurationDataService = configurationDataService;
		}

		#endregion

		#region Public Methods and Operators

		public BlogConfigurationDto GetConfiguration()
		{
			BlogConfigurationDto result = this.cacheProvider.Get<BlogConfigurationDto>("dexter.blog.configurationDto");

			if (result == null)
			{
				result = this.configurationDataService.GetConfiguration();
				this.cacheProvider.Put("dexter.blog.configurationDto", result, TimeSpan.FromHours(3));
			}

			return result;
		}

		public async Task<BlogConfigurationDto> GetConfigurationAsync()
		{
			BlogConfigurationDto result = await this.cacheProvider.GetAsync<BlogConfigurationDto>("dexter.blog.configurationDto");

			if (result == null)
			{
				Task.Run(() =>
					{
						result = this.configurationDataService.GetConfiguration();

						this.cacheProvider.PutAsync("dexter.blog.configurationDto", result, TimeSpan.FromHours(3));
					});
			}

			return result;
		}

		#endregion
	}
}