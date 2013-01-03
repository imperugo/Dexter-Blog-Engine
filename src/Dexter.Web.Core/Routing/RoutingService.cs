#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RoutingService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2013/01/03
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
		#region Fields

		private readonly ISetupService setupService;

		#endregion

		#region Constructors and Destructors

		public RoutingService(ISetupService setupService)
		{
			this.setupService = setupService;
		}

		#endregion

		#region Public Methods and Operators

		public void RegisterRoutes()
		{
			RouteCollection routes = RouteTable.Routes;
			HttpConfiguration webApiConfiguration = GlobalConfiguration.Configuration;

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			if (!this.setupService.IsInstalled)
			{
				routes.Add(new SetupRoute());
				return;
			}

			webApiConfiguration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new
				                                                                               {
					                                                                               id = RouteParameter.Optional
				                                                                               });

			routes.MapRoute("Default", "{controller}/{action}/{id}", 
				new
					{
						controller = "Home", 
						action = "Index", 
						id = UrlParameter.Optional
					}, 
				new[] { "Dexter.Host.Controllers" });
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