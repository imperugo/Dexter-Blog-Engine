#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterApplication.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Web.Core.HttpApplication
{
	using System;
	using System.Net;
	using System.Web;
	using System.Web.Http;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Async;
	using Dexter.Async.TaskExecutor;
	using Dexter.Dependency;
	using Dexter.Services;
	using Dexter.Shared.Exceptions;
	using Dexter.Web.Core.Routing;

	public class DexterApplication : HttpApplication
	{
		#region Fields

		private readonly IDexterContainer container;

		private readonly IDexterCall dexterCall;

		private readonly ILog logger;

		private readonly IRoutingService routingService;

		private readonly ITaskExecutor taskExecutor;

		private readonly IPluginService pluginService;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Web.HttpApplication"/> class.
		/// </summary>
		public DexterApplication()
		{
			this.logger = LogManager.GetCurrentClassLogger();

			DexterContainer.StartUp();
			this.container = DexterContainer.Resolve<IDexterContainer>();
			this.dexterCall = DexterContainer.Resolve<IDexterCall>();
			this.routingService = DexterContainer.Resolve<IRoutingService>();
			this.taskExecutor = DexterContainer.Resolve<ITaskExecutor>();
			this.pluginService = DexterContainer.Resolve<IPluginService>();
			this.pluginService.LoadAllEnabledPlugins();

			base.BeginRequest += (o, args) => this.BeginRequest();
			base.EndRequest += (o, args) => this.EndRequest();
		}

		#endregion

		#region Public Methods and Operators

		public new void BeginRequest()
		{
			this.logger.DebugFormat("Beginning request for url '{0}'",HttpContext.Current.Request.Url);
			this.dexterCall.StartSession();
		}

		public new void EndRequest()
		{
			Exception errors = this.Server.GetLastError();

			bool succesfully = errors == null || errors is DexterRestartRequiredException;
			this.dexterCall.Complete(succesfully);

			if (succesfully)
			{
				this.taskExecutor.StartExecuting();
			}
			else
			{
				this.taskExecutor.Discard();
			}

			this.logger.DebugFormat("Ending request for url '{0}'", HttpContext.Current.Request.Url);

			var exception = errors as DexterRestartRequiredException;
			
			if (exception != null)
			{
				this.logger.DebugFormat("Restart required by '{0}' with reason '{1}'.", exception.Caller, exception.Message);
				HttpRuntime.UnloadAppDomain();
			}
		}

		public new void Error()
		{
			Exception exception = this.Server.GetLastError();

			if (exception is DexterDatabaseConnectionException)
			{
				this.logger.Fatal(exception.Message, exception);
				HttpContext.Current.Response.Redirect("~/Dxt-Admin/Error/DatatabaseConnectionError");
				return;
			}

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

			this.Server.ClearError();
		}

		public new void Init()
		{
			IHttpModule[] modules = this.container.ResolveAll<IHttpModule>();

			foreach (IHttpModule httpModule in modules)
			{
				httpModule.Init(this);
			}
		}

		#endregion

		#region Methods

		protected void Application_End(object sender, EventArgs e)
		{
			this.container.Shutdown();
		}

		protected void Application_Start(object sender, EventArgs e)
		{
			this.routingService.RegisterRoutes();

			this.RegisterGlobalFilters(GlobalFilters.Filters);

			DependencyResolver.SetResolver(this.container.Resolve<IDependencyResolver>());
			GlobalConfiguration.Configuration.DependencyResolver = this.container.Resolve<System.Web.Http.Dependencies.IDependencyResolver>();
		}

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