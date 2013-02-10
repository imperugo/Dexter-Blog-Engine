#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Navigation.Concretes
{
	using System.Collections.Generic;
	using System.Globalization;

	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class PostUrlBuilder : UrlBuilderBase, IPostUrlBuilder
	{
		#region Constructors and Destructors

		public PostUrlBuilder(IConfigurationService configurationService)
			: base(configurationService)
		{
		}

		#endregion

		#region Public Methods and Operators

		public SiteUrl ArchivePosts(int month, int year)
		{
			string[] segments = new[]
				                    {
					                    year.ToString(CultureInfo.InvariantCulture), 
					                    month.ToString(CultureInfo.InvariantCulture)
				                    };

			return new SiteUrl(this.Domain, this.HttpPort, false, null, "Blog", "Archive", segments, null);
		}

		public SiteUrl Delete(ItemDto item)
		{
			string[] segments = new[]
				                    {
					                    item.PublishAt.Date.Year.ToString(CultureInfo.InvariantCulture), 
					                    item.PublishAt.Date.Month.ToString(CultureInfo.InvariantCulture), 
					                    item.PublishAt.Date.Day.ToString(CultureInfo.InvariantCulture), 
					                    item.Slug
				                    };

			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Post", "Detele", segments, null);
		}

		public SiteUrl Edit(ItemDto item)
		{
			string id = item != null ? item.Id.ToString(CultureInfo.InvariantCulture) : null;

			string[] segments = new[]
				                    {
					                    id
				                    };

			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Post", "Manage", segments, null);
		}

		public SiteUrl Permalink(ItemDto item)
		{
			string[] segments = new[]
				                    {
					                    item.PublishAt.Date.Year.ToString(CultureInfo.InvariantCulture), 
					                    item.PublishAt.Date.Month.ToString(CultureInfo.InvariantCulture), 
					                    item.PublishAt.Date.Day.ToString(CultureInfo.InvariantCulture), 
					                    item.Slug
				                    };

			return new SiteUrl(this.Domain, this.HttpPort, false, null, "Blog", "Post", segments, null);
		}

		public SiteUrl TrackBack(ItemDto item)
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, null, "Services", "Trackback", null, new Dictionary<string, string>
				                                                                                           {
					                                                                                           { "id", item.Id.ToString(CultureInfo.InvariantCulture) }, 
					                                                                                           { "itemType", "Post" }
				                                                                                           });
		}

		#endregion
	}
}