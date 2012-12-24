#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterControllerActivator.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency.Web.Mvc
{
	using System;
	using System.Web.Mvc;
	using System.Web.Routing;

	public class DexterControllerActivator : IControllerActivator
	{
		#region Fields

		private readonly IDexterContainer container;

		#endregion

		#region Constructors and Destructors

		public DexterControllerActivator(IDexterContainer container)
		{
			this.container = container;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	When implemented in a class, creates a controller.
		/// </summary>
		/// <returns> The created controller. </returns>
		/// <param name="requestContext"> The request context. </param>
		/// <param name="controllerType"> The controller type. </param>
		public IController Create(RequestContext requestContext, Type controllerType)
		{
			return (IController)this.container.Resolve(controllerType);
		}

		#endregion
	}
}