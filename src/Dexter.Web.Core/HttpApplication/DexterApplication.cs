#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterApplication.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.HttpApplication
{
	using System;
	using System.Net;
	using System.Reflection;
	using System.Web;
	using System.Web.Http;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Dependency;
	using Dexter.Web.Core.Routing;

	public class DexterApplication : IDexterApplication
	{
		#region Fields

		private readonly IDexterContainer container;

		private readonly ILog logger;

		private readonly IRoutingService routingService;

		#endregion

		#region Constructors and Destructors

		public DexterApplication(IRoutingService routingService, IDexterContainer container, ILog logger)
		{
			this.routingService = routingService;
			this.container = container;
			this.logger = logger;
		}

		#endregion

		#region Public Methods and Operators

		public void ApplicationEnd()
		{
			var runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);

			if (runtime != null)
			{
				var shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
				var shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);

				if (shutDownMessage.Contains("HostingEnvironment initiated shutdown"))
				{
					//The normal app domain recycle should be logged as a info
					logger.InfoFormat("{0} stack = {1}", shutDownMessage, shutDownStack);
				}
				else
				{
					//maybe there is a problem :)
					logger.WarnFormat("{0} stack = {1}", shutDownMessage, shutDownStack);
				}
			}

			logger.Info("Application shutdown");
			container.Shutdown();
		}

		public void ApplicationError(HttpApplication application)
		{
			Exception exception = application.Server.GetLastError();

			if (exception is HttpException)
			{
				HttpException httpException = (HttpException)exception;
				if (httpException.GetHttpCode() == (int)HttpStatusCode.NotFound)
				{
					logger.Debug("Web resource not found", httpException);
				}
			}
			else
			{
				logger.Error("Unhandled Exception!", exception);
			}

			application.Server.ClearError();
		}

		public void ApplicationStart()
		{
			AreaRegistration.RegisterAllAreas();

			routingService.RegisterRoutes();

			RegisterGlobalFilters(GlobalFilters.Filters);
			DependencyResolver.SetResolver(this.container.Resolve<IDependencyResolver>());
			GlobalConfiguration.Configuration.DependencyResolver = this.container.Resolve<System.Web.Http.Dependencies.IDependencyResolver>();
		}

		public void AuthenticateRequest()
		{
		}

		public void Init(HttpApplication application)
		{
			IHttpModule[] modules = this.container.ResolveAll<IHttpModule>();

			foreach (IHttpModule httpModule in modules)
			{
				httpModule.Init(application);
			}
		}

		#endregion

		private void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			var f = container.ResolveAll<FilterAttribute>();

			foreach (var filter in f)
			{
				filters.Add(filter);
			}
		}
	}
}