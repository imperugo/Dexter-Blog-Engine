#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterApplication.cs
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

namespace Dexter.Web.Core.HttpApplication
{
	using System;
	using System.Net;
	using System.Reflection;
	using System.Web;
	using System.Web.Http;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Async;
	using Dexter.Dependency;
	using Dexter.Web.Core.Routing;

	public class DexterApplication : IDexterApplication
	{
		#region Fields

		private readonly IDexterContainer container;

		private readonly IDexterCall dexterCall;

		private readonly ILog logger;

		private readonly IRoutingService routingService;

		#endregion

		#region Constructors and Destructors

		public DexterApplication(IRoutingService routingService, IDexterContainer container, ILog logger, IDexterCall dexterCall)
		{
			this.routingService = routingService;
			this.container = container;
			this.logger = logger;
			this.dexterCall = dexterCall;
		}

		#endregion

		#region Public Methods and Operators

		public void ApplicationEnd()
		{
			HttpRuntime runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);

			if (runtime != null)
			{
				string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
				string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);

				if (shutDownMessage.Contains("HostingEnvironment initiated shutdown"))
				{
					// The normal app domain recycle should be logged as a info
					this.logger.InfoFormat("{0} stack = {1}", shutDownMessage, shutDownStack);
				}
				else
				{
					// maybe there is a problem :)
					this.logger.WarnFormat("{0} stack = {1}", shutDownMessage, shutDownStack);
				}
			}

			this.logger.Info("Application shutdown");
			this.container.Shutdown();
		}

		public void ApplicationError(HttpApplication application)
		{
			Exception exception = application.Server.GetLastError();

			if (exception is HttpException)
			{
				HttpException httpException = (HttpException)exception;
				if (httpException.GetHttpCode() == (int)HttpStatusCode.NotFound)
				{
					this.logger.Debug("Web resource not found", httpException);
				}
			}
			else
			{
				this.logger.Error("Unhandled Exception!", exception);
			}

			application.Server.ClearError();
		}

		public void ApplicationStart()
		{
			AreaRegistration.RegisterAllAreas();

			this.routingService.RegisterRoutes();

			this.RegisterGlobalFilters(GlobalFilters.Filters);

			DependencyResolver.SetResolver(this.container.Resolve<IDependencyResolver>());
			GlobalConfiguration.Configuration.DependencyResolver = this.container.Resolve<System.Web.Http.Dependencies.IDependencyResolver>();
		}

		public void AuthenticateRequest()
		{
		}

		public void BeginRequest(HttpApplication application)
		{
			this.dexterCall.StartSession();
		}

		public void EndRequest(HttpApplication application)
		{
			Exception errors = application.Server.GetLastError();

			this.dexterCall.Complete(errors == null);
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

		#region Methods

		private void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			FilterAttribute[] f = this.container.ResolveAll<FilterAttribute>();

			foreach (FilterAttribute filter in f)
			{
				filters.Add(filter);
			}
		}

		#endregion
	}
}