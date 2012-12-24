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
	using System.Web;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class UrlBuilder : IUrlBuilder
	{
		#region Fields

		private readonly IConfigurationService configurationService;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		public UrlBuilder(ILog logger, IAdminUrlBuilder adminUrlBuilder, IConfigurationService configurationService, IPostUrlBuilder postUrlBuilder)
		{
			this.logger = logger;
			this.Admin = adminUrlBuilder;
			this.Post = postUrlBuilder;
			this.configurationService = configurationService;
		}

		#endregion

		#region Public Properties

		public IAdminUrlBuilder Admin { get; private set; }

		public virtual SiteUrl Home
		{
			get
			{
				BlogConfigurationDto conf = this.configurationService.GetConfiguration();

				return new SiteUrl(conf.DefaultDomain, conf.DefaultHttpsPort, false, null, "Home", "Index", null);
			}
		}

		public IPostUrlBuilder Post { get; set; }

		#endregion

		#region Public Methods and Operators

		public SiteUrl CurrentUrl(HttpContextWrapper request)
		{
			string area = request.Request.RequestContext.RouteData.Values["area"] != null ? request.Request.RequestContext.RouteData.Values["area"].ToString() : null;

			return new SiteUrl(request.Request.Url.Host, request.Request.Url.Port, request.Request.IsSecureConnection, area, request.Request.RequestContext.RouteData.Values["controller"].ToString(), request.Request.RequestContext.RouteData.Values["action"].ToString(), null);
		}

		public SiteUrl PingbackUrl()
		{
			return new SiteUrl(this.Home.Domain, this.Home.Port, this.Home.IsSecureConnection, this.Home.Area, "Services", "Pingback", null);
		}

		#endregion
	}
}