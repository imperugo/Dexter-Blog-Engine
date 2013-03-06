#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterControllerFactory.cs
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

namespace Dexter.Dependency.Web.Mvc
{
	using System;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;

	using Common.Logging;

	/// <summary>
	/// Defines a controller provider for all MVC controllers.
	/// </summary>
	public class DexterControllerFactory : DefaultControllerFactory
	{
		#region Fields

		private readonly IDexterContainer container;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Web.Mvc.DefaultControllerFactory" /> class.
		/// </summary>
		/// <param name="logger"> The logger. </param>
		/// <param name="container"> The container. </param>
		public DexterControllerFactory(ILog logger, IDexterContainer container)
		{
			this.logger = logger;
			this.container = container;
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Web.Mvc.DefaultControllerFactory" /> class using a controller activator.
		/// </summary>
		/// <param name="controllerActivator"> An object that implements the controller activator interface. </param>
		/// <param name="logger"> The logger. </param>
		/// <param name="container"> The container. </param>
		public DexterControllerFactory(IControllerActivator controllerActivator, ILog logger, IDexterContainer container)
			: base(controllerActivator)
		{
			this.logger = logger;
			this.container = container;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Creates the specified controller by using the specified request context.
		/// </summary>
		/// <param name="requestContext"> The context of the HTTP request, which includes the HTTP context and route data. </param>
		/// <param name="controllerName"> The name of the controller. </param>
		/// <returns> A reference to the controller. </returns>
		/// <exception cref="T:System.ArgumentNullException">The
		/// 	<paramref name="requestContext" />
		/// 	parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="controllerName" />parameter is null or empty.</exception>
		public override IController CreateController(RequestContext requestContext, string controllerName)
		{
			Type controllerType = this.GetControllerType(requestContext, controllerName);

			if (controllerType == null)
			{
				throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
			}

			this.logger.DebugFormat("Releasing controller '{0}'", controllerType);
			
			return this.GetControllerInstance(requestContext, controllerType);
		}

		/// <summary>
		/// 	Releases the specified controller.
		/// </summary>
		/// <param name="controller"> The controller to release. </param>
		public override void ReleaseController(IController controller)
		{
			// NOTE:The relese method of container will call the dispose id the instance implements IDisposable
			this.container.Release(controller);
			this.logger.Debug("ReleaseController");
		}

		#endregion

		#region Methods

		/// <summary>
		/// 	Retrieves the controller instance for the specified request context and controller type.
		/// </summary>
		/// <param name="requestContext"> The context of the HTTP request, which includes the HTTP context and route data. </param>
		/// <param name="controllerType"> The type of the controller. </param>
		/// <returns> The controller instance. </returns>
		/// <exception cref="T:System.Web.HttpException"><paramref name="controllerType" />is null.</exception>
		/// <exception cref="T:System.ArgumentException"><paramref name="controllerType" />cannot be assigned.</exception>
		/// <exception cref="T:System.InvalidOperationException">An instance of <paramref name="controllerType" />cannot be created.</exception>
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
			}

			if (this.container.HasComponent(controllerType))
			{
				throw new InvalidOperationException("Unable to release the specified controller");
			}

			this.logger.DebugFormat("Resolving controller {0}", controllerType);
			return this.container.Resolve(controllerType) as IController;
		}

		#endregion
	}
}