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

	using Dexter.Services;
	using Dexter.Web.Core.Routing.Routes;

	public class RoutingService : IRoutingService
	{
		private readonly ISetupService setupService;

		public RoutingService(ISetupService setupService)
		{
			this.setupService = setupService;
		}

		#region Public Methods and Operators

		public void RegisterRoutes()
		{
			RouteCollection routes = RouteTable.Routes;
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			if (!setupService.IsInstalled)
			{
				routes.Add(new SetupRoute());
				return;
			}

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
			RouteCollection routes = RouteTable.Routes;
			routes.Clear();

			this.RegisterRoutes();
		}

		#endregion
	}
}