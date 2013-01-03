#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			UrlBuilderBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/24
// Last edit:	2012/12/31
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Helpers
{
	using System.Web;

	using Dexter.Entities;
	using Dexter.Services;

	public class UrlBuilderBase
	{
		#region Fields

		private readonly IConfigurationService configuration;

		private readonly int httpPort;

		private readonly int httpsPort;

		#endregion

		#region Constructors and Destructors

		public UrlBuilderBase(IConfigurationService configurationService)
		{
			this.configuration = configurationService;
			this.httpPort = 80;
			this.httpsPort = 443;
		}

		#endregion

		#region Public Properties

		public BlogConfigurationDto Configuration
		{
			get
			{
				return this.configuration.GetConfiguration();
			}
		}

		public string Domain
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.Request.Url.Host;
				}

				return this.Configuration.SiteDomain.Host;
			}
		}

		public int HttpPort
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.Request.Url.Port;
				}

				return this.httpPort;
			}
		}

		public int HttpsPort
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.Request.Url.Port;
				}

				return this.httpsPort;
			}
		}

		#endregion
	}
}