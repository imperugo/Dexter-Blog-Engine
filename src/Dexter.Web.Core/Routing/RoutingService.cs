#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RoutingService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Routing
{
	using System.Web.Http;
	using System.Web.Mvc;
	using System.Web.Routing;

	public class RoutingService : IRoutingService
	{
		#region Public Methods and Operators

		public void RegisterRoutes()
		{
			RouteCollection routes = RouteTable.Routes;
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", 
				"{controller}/{action}/{id}", 
				new
					          {
						          controller = "Home", 
						          action = "Index", 
						          id = UrlParameter.Optional
					          },
				new[] { "Dexter.Host.Controllers" });

			HttpConfiguration webApiConfiguration = GlobalConfiguration.Configuration;

			webApiConfiguration.Routes.MapHttpRoute(
				"DefaultApi",
				"api/{controller}/{id}",
				new
					          {
						          id = RouteParameter.Optional
					          });
		}

		public void UpdateRoutes()
		{
		}

		#endregion
	}
}