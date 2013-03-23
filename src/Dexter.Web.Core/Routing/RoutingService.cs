#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RoutingService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/03/23
// License:		New BSD License (BSD)
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

	using Common.Logging;

	using Dexter.Dependency.Extensions;
	using Dexter.Services;
	using Dexter.Web.Core.Routing.Routes;

	public class RoutingService : IRoutingService
	{
		#region Fields

		private readonly ILog logger;

		private readonly IPageService pageService;

		private readonly ISetupService setupService;

		#endregion

		#region Constructors and Destructors

		public RoutingService(ISetupService setupService, IPageService pageService, ILog logger)
		{
			this.setupService = setupService;
			this.pageService = pageService;
			this.logger = logger;
		}

		#endregion

		#region Public Methods and Operators

		public void RegisterRoutes()
		{
			AreaRegistration.RegisterAllAreas();

			RouteCollection routes = RouteTable.Routes;
			HttpConfiguration webApiConfiguration = GlobalConfiguration.Configuration;

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("robots.txt");
			routes.IgnoreRoute("wlwmanifest.xml");

			if (!this.setupService.IsInstalled)
			{
				routes.Add(new SetupRoute());
				return;
			}

			string[] slugs = this.pageService.GetAllSlugs();

			slugs.ForEach(x =>
				{
					this.logger.DebugFormat("Registering route for dynamic page '{0}'", x);
					routes.MapRoute(string.Concat("Page_", x), 
						x, 
						new
							{
								controller = "Page", 
								action = "Index", 
								id = x
							}, 
						new[] { "Dexter.Host.Controllers" });
					this.logger.DebugFormat("Registered route for dynamic page '{0}'", x);
				});

			webApiConfiguration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new
				                                                                               {
					                                                                               id = RouteParameter.Optional
				                                                                               });

			routes.Add(new Route("wlw/metaweblog", new MetaWeblogApiRouteHandler()));
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