#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/27
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency.Castle
{
	using System.Web;
	using System.Web.Http.Dispatcher;
	using System.Web.Mvc;

	using global::Castle.MicroKernel.Lifestyle;

	using Dexter.Dependency.Installation;
	using Dexter.Dependency.Web.Mvc;

	public class CastleInstaller : ILayerInstaller
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name="container"> The container. </param>
		public void ApplicationStarted(IDexterContainer container)
		{
		}

		/// <summary>
		/// 	Installs all the component needed by the assembly.
		/// </summary>
		/// <param name="container"> The container. </param>
		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<IHttpModule, PerWebRequestLifestyleModule>(LifeCycle.Singleton);
			container.Register<IDependencyResolver, DexterDependencyResolver>(LifeCycle.Singleton);
			container.Register<System.Web.Http.Dependencies.IDependencyResolver, Web.WebApi.DexterDependencyResolver>(LifeCycle.Singleton);
			container.Register<IHttpControllerActivator, Web.WebApi.DexterHttpControllerActivator>(LifeCycle.Singleton);
		}

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name="container"> The container. </param>
		public void ServiceRegistrationComplete(IDexterContainer container)
		{
		}

		#endregion
	}
}