#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			UrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Navigation.Concretes
{
	using System.Collections;
	using System.Web;

	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class UrlBuilder : UrlBuilderBase, IUrlBuilder
	{
		#region Fields

		#endregion

		#region Constructors and Destructors

		public UrlBuilder(IConfigurationService configurationService, IAdminUrlBuilder admin, IPostUrlBuilder post, IPageUrlBuilder page)
			: base(configurationService)
		{
			this.Admin = admin;
			this.Post = post;
			this.Page = page;
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

		public IPostUrlBuilder Post { get; private set; }

		public IPageUrlBuilder Page { get; private set; }

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

		#endregion
	}
}