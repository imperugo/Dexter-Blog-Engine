#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AdminUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/12/26
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Concretes
{
	using System.Collections.Generic;

	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class AdminUrlBuilder : UrlBuilderBase, IAdminUrlBuilder
	{
		#region Fields

		private readonly IPageUrlBuilder pageUrlBuilder;

		private readonly IPostUrlBuilder postUrlBuilder;

		#endregion

		#region Constructors and Destructors

		public AdminUrlBuilder(IConfigurationService configurationService, IPostUrlBuilder postUrlBuilder, IPageUrlBuilder pageUrlBuilder)
			: base(configurationService)
		{
			this.postUrlBuilder = postUrlBuilder;
			this.pageUrlBuilder = pageUrlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		public SiteUrl DeletePage(ItemDto item)
		{
			return this.pageUrlBuilder.Delete(item);
		}

		public SiteUrl DeletePost(ItemDto item)
		{
			return this.postUrlBuilder.Delete(item);
		}

		public SiteUrl EditPage(ItemDto item)
		{
			return this.pageUrlBuilder.Edit(item);
		}

		public SiteUrl EditPost(ItemDto item)
		{
			return this.postUrlBuilder.Edit(item);
		}

		public SiteUrl FeedbackPage(FeedbackType feedback, string localizationKey, SiteUrl redirect)
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Feedback", "Index", new[]
				                                                                                        {
					                                                                                        feedback.ToString(), 
					                                                                                        localizationKey
				                                                                                        }, new Dictionary<string, object>
					                                                                                           {
						                                                                                           { "url", redirect }
					                                                                                           });
		}

		public SiteUrl Home()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Home", "Index", null, null);
		}

		public SiteUrl Post()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Post", "Index", null, null);
		}

		public SiteUrl Login()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Login", "Index", null, null);
		}

		#endregion
	}
}