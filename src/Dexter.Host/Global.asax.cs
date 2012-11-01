#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Global.asax.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host
{
	using System;
	using System.Web;
	using System.Web.Http;
	using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;

	using Dexter.Dependency;
	using Dexter.Web.Core.HttpApplication;

	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class WebApiApplication : HttpApplication
	{
		#region Static Fields

		private static readonly IDexterApplication DexterApplication;

		#endregion

		#region Constructors and Destructors

		static WebApiApplication()
		{
			DexterContainer.StartUp();
			DexterApplication = DexterContainer.Resolve<IDexterApplication>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Web.HttpApplication"/> class.
		/// </summary>
		public WebApiApplication()
		{
			this.BeginRequest += (sender, args) =>
				{
					HttpApplication app = (HttpApplication)sender;
					HttpContext context = app.Context;

					// Attempt to peform first request initialization
					FirstRequestInitialization.Initialize(context);
				};

			this.EndRequest += (sender, args) => DexterApplication.ApplicationEnd();

			this.Error += (sender, args) => DexterApplication.ApplicationError(this);

			this.AuthenticateRequest += (sender, args) => DexterApplication.AuthenticateRequest();
		}

		#endregion

		#region Public Methods and Operators

		public override void Init()
		{
			base.Init();
			DexterApplication.Init(this);
		}

		#endregion

		#region Methods

		protected void Application_Start(object sender, EventArgs e)
		{
			DexterApplication.ApplicationStart();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		#endregion
	}

	internal class FirstRequestInitialization
	{
		#region Static Fields

		private static readonly object Lock = new object();

		private static bool initializedAlready;

		#endregion

		// Initialize only on the first request
		#region Public Methods and Operators

		public static void Initialize(HttpContext context)
		{
			if (initializedAlready)
			{
				return;
			}

			lock (Lock)
			{
				if (initializedAlready)
				{
					return;
				}

				InizializeWithContext();
				initializedAlready = true;
			}
		}

		#endregion

		#region Methods

		private static void InizializeWithContext()
		{
		}

		#endregion
	}
}