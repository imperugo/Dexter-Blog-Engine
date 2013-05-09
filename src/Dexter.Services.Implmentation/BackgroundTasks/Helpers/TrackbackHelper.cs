#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TrackbackHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/31
// Last edit:	2013/03/31
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Implmentation.BackgroundTasks.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	using Common.Logging;

	using Dexter.Dependency;
	using Dexter.Shared.Dto;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;

	internal class TrackbackHelper
	{
		#region Static Fields

		/// <summary>
		///     Regex used to find the trackback link on a remote web page.
		/// </summary>
		private static readonly Regex trackbackLinkRegex = new Regex("trackback:ping=\"([^\"]+)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// private static readonly Regex urlsRegex = new Regex(@"\<a\s+href=""(http://.*?)"".*\>.+\<\/a\>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		// private static readonly Regex urlsRegex = new Regex(@"<a[^(href)]?href=""([^""]+)""[^>]?>([^<]+)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		private static IConfigurationService configurationService;

		private static IUrlBuilder urlBuilder;

		private static ILog logger;

		#endregion

		#region Constructors and Destructors

		public TrackbackHelper()
		{
			urlBuilder = DexterContainer.Resolve<IUrlBuilder>();
			configurationService = DexterContainer.Resolve<IConfigurationService>();
			logger = LogManager.GetCurrentClassLogger();
		}

		#endregion

		#region Public Methods and Operators

		public static async Task<bool> Notify(ItemDto item, Uri uri)
		{
			BlogConfigurationDto configuration = configurationService.GetConfiguration();

			using (HttpClient client = new HttpClient())
			{
				string content = await client.GetStringAsync(uri);
				Uri trackbackUrl = GetTrackBackUrlFromPage(content);

				if (trackbackUrl != null)
				{
					SiteUrl itemUrl = item is PostDto
						                  ? urlBuilder.Post.Permalink(item)
						                  : urlBuilder.Page.Permalink(item);

					Dictionary<string, string> form = new Dictionary<string, string>
						                                  {
							                                  { "title", item.Title }, 
							                                  { "url", itemUrl.ToString() }, 
							                                  { "excerpt", item.Excerpt }, 
							                                  { "blog_name", configuration.Name }
						                                  };

					try
					{
						await client.PostAsync(trackbackUrl.ToString(), new FormUrlEncodedContent(form));
						return true;
					}
					catch (Exception ex)
					{
						logger.WarnFormat("Unable to send trackback to '{0}'", ex, uri);
					}

					return false;
				}

				return false;
			}
		}

		#endregion

		#region Methods

		private static Uri GetTrackBackUrlFromPage(string input)
		{
			string url = trackbackLinkRegex.Match(input).Groups[1].ToString().Trim();
			Uri uri;

			return Uri.TryCreate(url, UriKind.Absolute, out uri) ? uri : null;
		}

		#endregion
	}
}