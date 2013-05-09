#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			UrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/28
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Navigation.Concretes
{
	using System.Collections;
	using System.Web;

	using Dexter.Shared.Dto;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class UrlBuilder : UrlBuilderBase, IUrlBuilder
	{
		#region Constructors and Destructors

		public UrlBuilder(IConfigurationService configurationService, IAdminUrlBuilder admin, IPostUrlBuilder post, IPageUrlBuilder page, ICategoryUrlBuilder categoryUrlBuilder, IServiceUrlBuilder serviceUrlBuilder, IFeedUrlBuilder feedUrlBuilder)
			: base(configurationService)
		{
			this.Admin = admin;
			this.Post = post;
			this.Page = page;
			this.Category = categoryUrlBuilder;
			this.Service = serviceUrlBuilder;
			this.Feed = feedUrlBuilder;
		}

		#endregion

		#region Public Properties

		public IAdminUrlBuilder Admin { get; private set; }

		public virtual SiteUrl Home
		{
			get
			{
				return new SiteUrl(this.Domain, this.HttpPort, false, null, "Home", "Index", null, null);
			}
		}

		public IPageUrlBuilder Page { get; private set; }

		public IPostUrlBuilder Post { get; private set; }

		public ICategoryUrlBuilder Category { get; private set; }

		public IServiceUrlBuilder Service { get; private set; }

		public IFeedUrlBuilder Feed { get; private set; }

		#endregion

		#region Public Methods and Operators

		public SiteUrl CurrentUrl(HttpContextBase request)
		{
			string area = request.Request.RequestContext.RouteData.Values["area"] != null ? request.Request.RequestContext.RouteData.Values["area"].ToString() : null;

			return new SiteUrl(this.Domain, this.HttpPort, request.Request.IsSecureConnection, area, request.Request.RequestContext.RouteData.Values["controller"].ToString(), request.Request.RequestContext.RouteData.Values["action"].ToString(), null, request.Request.QueryString.ToDictionary());
		}

		public SiteUrl PingbackUrl()
		{
			BlogConfigurationDto conf = this.Configuration;

			return new SiteUrl(this.Domain, this.HttpPort, false, this.Home.Area, "Services", "Pingback", null, null);
		}

		public string ResolveUrl(string value)
		{
			if (value == null)
			{
				return null;
			}

			// *** Absolute path - just return
			if (value.IndexOf("://") != -1)
			{
				return value;
			}

			// *** Fix up image path for ~ root app dir directory
			if (value.StartsWith("~"))
			{
				return VirtualPathUtility.ToAbsolute(value);
			}

			return value;
		}

		#endregion
	}
}