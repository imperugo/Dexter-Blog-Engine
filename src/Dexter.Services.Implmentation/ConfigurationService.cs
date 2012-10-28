#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ConfigurationService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/10/28
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

		private readonly IBlogConfigurationDataService blogConfigurationDataService;

		private readonly ICacheProvider cacheProvider;

		#endregion

		#region Constructors and Destructors

		public ConfigurationService(ICacheProvider cacheProvider, IBlogConfigurationDataService blogConfigurationDataService)
		{
			this.cacheProvider = cacheProvider;
			this.blogConfigurationDataService = blogConfigurationDataService;
		}

		#endregion

		#region Public Methods and Operators

		public BlogConfiguration GetConfiguration()
		{
			BlogConfiguration result = this.cacheProvider.Get<BlogConfiguration>("dexter.blog.configuration");

			if (result == null)
			{
				result = this.blogConfigurationDataService.GetConfiguration();
				this.cacheProvider.Put("dexter.blog.configuration", result, TimeSpan.FromHours(3));
			}

			return result;
		}

		public async Task<BlogConfiguration> GetConfigurationAsync()
		{
			BlogConfiguration result = await this.cacheProvider.GetAsync<BlogConfiguration>("dexter.blog.configuration");

			if (result == null)
			{
				Task.Run(() =>
					{
						result = this.blogConfigurationDataService.GetConfiguration();

						this.cacheProvider.PutAsync("dexter.blog.configuration", result, TimeSpan.FromHours(3));
					});
			}

			return result;
		}

		#endregion
	}
}