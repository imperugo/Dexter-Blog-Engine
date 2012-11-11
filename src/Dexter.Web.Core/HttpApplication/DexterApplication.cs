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
	using System.Web;
	using System.Web.Http;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Async;
	using Dexter.Async.TaskExecutor;
	using Dexter.Dependency;
	using Dexter.Web.Core.Routing;

	public class DexterApplication : HttpApplication
	{
		#region Fields

		private readonly IDexterContainer container;

		private readonly IDexterCall dexterCall;

		private readonly ILog logger;

		private readonly IRoutingService routingService;

		private readonly ITaskExecutor taskExecutor;

		#endregion

		#region Constructors and Destructors

		static DexterApplication()
		{
			DexterContainer.StartUp();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Web.HttpApplication"/> class.
		/// </summary>
		public DexterApplication()
		{
			this.container = DexterContainer.Resolve<IDexterContainer>();
			this.dexterCall = DexterContainer.Resolve<IDexterCall>();
			this.routingService = DexterContainer.Resolve<IRoutingService>();
			this.taskExecutor = DexterContainer.Resolve<ITaskExecutor>();
			this.logger = LogManager.GetCurrentClassLogger();

			base.BeginRequest += (o, args) => this.BeginRequest();
			base.EndRequest += (o, args) => this.EndRequest();
		}

		#endregion

		#region Public Methods and Operators

		public new void BeginRequest()
		{
			this.dexterCall.StartSession();
		}

		public new void EndRequest()
		{
			Exception errors = this.Server.GetLastError();

			this.dexterCall.Complete(errors == null);

			this.taskExecutor.StartExecuting();
		}

		public new void Error()
		{
			Exception exception = this.Server.GetLastError();

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
			AreaRegistration.RegisterAllAreas();

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