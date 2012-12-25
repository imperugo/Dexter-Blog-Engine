#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PageUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/24
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Concretes
{
	using System.Globalization;

	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class PageUrlBuilder : UrlBuilderBase, IPageUrlBuilder
	{
		#region Constructors and Destructors

		public PageUrlBuilder(IConfigurationService configurationService)
			: base(configurationService)
		{
		}

		#endregion

		#region Public Methods and Operators

		public SiteUrl Delete(ItemDto item)
		{
			string[] segments = new[]
				                    {
					                    item.PublishAt.Date.Year.ToString(CultureInfo.InvariantCulture), 
					                    item.PublishAt.Date.Month.ToString(CultureInfo.InvariantCulture), 
					                    item.PublishAt.Date.Day.ToString(CultureInfo.InvariantCulture), 
					                    item.Slug
				                    };

			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Page", "Detele", segments, null);
		}

		public SiteUrl Edit(ItemDto item)
		{
			string[] segments = new[]
				                    {
					                    item.PublishAt.Date.Year.ToString(CultureInfo.InvariantCulture), 
					                    item.PublishAt.Date.Month.ToString(CultureInfo.InvariantCulture), 
					                    item.PublishAt.Date.Day.ToString(CultureInfo.InvariantCulture), 
					                    item.Slug
				                    };

			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Page", "Manage", segments, null);
		}

		public SiteUrl Permalink(ItemDto item)
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, null, "Blog", "Page", new[] { item.Slug }, null);
		}

		#endregion
	}
}