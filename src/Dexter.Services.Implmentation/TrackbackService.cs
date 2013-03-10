#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TrackbackService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/12
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
	using System.Net.Http;
	using System.Threading.Tasks;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Data.Exceptions;
	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;

	public class TrackbackService : ITrackbackService
	{
		#region Fields

		private readonly IPageDataService pageDataService;

		private readonly IPostDataService postDataService;

		private readonly ITrackBackDataService trackBackDataService;

		private readonly IUrlBuilder urlBuilder;

		private ILog logger;

		#endregion

		#region Constructors and Destructors

		public TrackbackService(ILog logger, IPageDataService pageDataService, IPostDataService postDataService, ITrackBackDataService trackBackDataService, IUrlBuilder urlBuilder)
		{
			this.logger = logger;
			this.pageDataService = pageDataService;
			this.postDataService = postDataService;
			this.trackBackDataService = trackBackDataService;
			this.urlBuilder = urlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		public async Task SaveOrUpdateAsync(TrackBackDto trackBack, ItemType itemType)
		{
			if (trackBack == null)
			{
				throw new ArgumentNullException("trackBack", "The trackback must contains an instance.");
			}

			if (string.IsNullOrEmpty(trackBack.Title))
			{
				throw new ArgumentException("Invalid trackback title.");
			}

			if (trackBack.Url == null)
			{
				throw new ArgumentException("Invalid trackback url.");
			}

			if (string.IsNullOrEmpty(trackBack.Name))
			{
				throw new ArgumentException("Invalid trackback name.");
			}

			if (!string.IsNullOrEmpty(trackBack.Title) && trackBack.ItemId > 0 && !string.IsNullOrEmpty(trackBack.Name) && trackBack.Url != null)
			{
				ItemDto item;
				if (itemType == ItemType.Post)
				{
					item = this.postDataService.GetPostByKey(trackBack.ItemId);
				}
				else
				{
					item = this.pageDataService.GetPageByKey(trackBack.ItemId);
				}

				if (item == null)
				{
					throw new ItemNotFoundException("trackBack.ItemId");
				}

				bool firstPingBack = this.trackBackDataService.IsFirstTrackbackBack(trackBack.ItemId, trackBack.Url);

				if (!firstPingBack)
				{
					throw new DuplicateTrackbackException(trackBack.ItemId, trackBack.Url, itemType);
				}

				bool isSpam = await this.IsSpam(trackBack, item);

				if (isSpam)
				{
					throw new SpamException();
				}
			}
		}

		#endregion

		#region Methods

		private async Task<bool> IsSpam(TrackBackDto trackBack, ItemDto item)
		{
			string response;
			using (HttpClient client = new HttpClient())
			{
				response = await client.GetStringAsync(trackBack.Url);
			}

			if (string.IsNullOrEmpty(response))
			{
				return false;
			}

			SiteUrl postUrl = this.urlBuilder.Post.Permalink(item);

			return !response.ToLowerInvariant().Contains(postUrl);
		}

		#endregion
	}

	public class SpamException : Exception
	{
	}

	public class DuplicateTrackbackException : Exception
	{
		#region Constructors and Destructors

		public DuplicateTrackbackException(int itemId, Uri url, ItemType itemType)
		{
			this.ItemId = itemId;
			this.Url = url;
			this.ItemType = itemType;
		}

		#endregion

		#region Public Properties

		public int ItemId { get; private set; }

		public ItemType ItemType { get; private set; }

		public Uri Url { get; private set; }

		#endregion
	}
}