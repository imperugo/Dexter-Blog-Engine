#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PublishedBackgroundTask.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/24
// Last edit:	2013/03/31
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Implmentation.BackgroundTasks
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	using Dexter.Async.TaskExecutor;
	using Dexter.Data;
	using Dexter.Dependency;
	using Dexter.Entities;
	using Dexter.Services.Implmentation.BackgroundTasks.Helpers;

	public class PublishedBackgroundTask : BackgroundTask
	{
		#region Static Fields

		/// <summary>
		///     Regex used to find all hyperlinks.
		/// </summary>
		private static readonly Regex urlsRegex = new Regex(@"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		#endregion

		#region Fields

		private readonly BlogConfigurationDto configuration;

		private readonly ItemDto item;

		#endregion

		#region Constructors and Destructors

		public PublishedBackgroundTask(ItemDto item)
			: this(item, DexterContainer.Resolve<IConfigurationDataService>())
		{
		}

		public PublishedBackgroundTask(ItemDto item, IConfigurationDataService configurationDataService)
		{
			this.configuration = configurationDataService.GetConfiguration();
			this.item = item;
		}

		#endregion

		#region Public Methods and Operators

		public override async Task Execute()
		{
			await PingHelper.NotifySites();

			IEnumerable<Uri> urlsFromContent = GetUrlsFromContent(this.item.Content);

			foreach (Uri uri in urlsFromContent)
			{
				bool trackbackSent = false;
				if (this.configuration.Tracking.EnableTrackBackSend)
				{
					trackbackSent = await TrackbackHelper.Notify(this.item, uri);
				}

				if (!trackbackSent && this.configuration.Tracking.EnablePingBackSend)
				{
					await PingbackHelper.Notify(this.item, uri);
				}
			}
		}

		#endregion

		#region Methods

		private static IEnumerable<Uri> GetUrlsFromContent(string content)
		{
			List<Uri> urlsList = new List<Uri>();
			foreach (string url in urlsRegex.Matches(content).Cast<Match>().Select(myMatch => myMatch.Groups["url"].ToString().Trim()))
			{
				Uri uri;
				if (Uri.TryCreate(url, UriKind.Absolute, out uri))
				{
					urlsList.Add(uri);
				}
			}

			return urlsList;
		}

		#endregion
	}
}