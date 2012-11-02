#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Global.asax.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/11/02
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
	using System.Web.Optimization;

	using Dexter.Dependency;
	using Dexter.Web.Core.HttpApplication;

	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class DexterHostApplication : HttpApplication
	{
		#region Static Fields

		private static readonly IDexterApplication DexterApplication;

		#endregion

		#region Constructors and Destructors

		static DexterHostApplication()
		{
			DexterContainer.StartUp();
			DexterApplication = DexterContainer.Resolve<IDexterApplication>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Web.HttpApplication"/> class.
		/// </summary>
		public DexterHostApplication()
		{
			this.BeginRequest += (sender, args) => DexterApplication.BeginRequest(this);

			this.EndRequest += (sender, args) => DexterApplication.EndRequest(this);

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

		protected void Application_End(object sender, EventArgs e)
		{
			DexterApplication.ApplicationEnd();
		}

		protected void Application_Start(object sender, EventArgs e)
		{
			DexterApplication.ApplicationStart();

			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		#endregion
	}
}