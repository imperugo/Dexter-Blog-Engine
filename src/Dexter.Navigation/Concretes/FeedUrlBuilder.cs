#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			FeedUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/27
// Last edit:	2013/04/27
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Concretes
{
	using System;

	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class FeedUrlBuilder : UrlBuilderBase, IFeedUrlBuilder
	{
		#region Constructors and Destructors

		public FeedUrlBuilder(IConfigurationService configurationService)
			: base(configurationService)
		{
		}

		#endregion

		#region Public Methods and Operators

		public SiteUrl CategoryFeed(string categoryName)
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "api", "Syndication", "ByCategory", new[] { "categoryName" }, null);
		}

		public SiteUrl MainFeed()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "api", "Syndication", "MainFeed", null, null);
		}

		public SiteUrl TagFeed(string tag)
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "api", "Syndication", "ByTag", new[] { "tag" }, null);
		}

		#endregion
	}
}