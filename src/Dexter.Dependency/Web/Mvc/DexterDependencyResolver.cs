#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterDependencyResolver.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency.Web.Mvc
{
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class DexterDependencyResolver : IDependencyResolver
	{
		#region Fields

		private readonly IDexterContainer container;

		#endregion

		#region Constructors and Destructors

		public DexterDependencyResolver(IDexterContainer container)
		{
			this.container = container;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Resolves singly registered services that support arbitrary object creation.
		/// </summary>
		/// <returns> The requested service or object. </returns>
		/// <param name="serviceType"> The type of the requested service or object. </param>
		public object GetService(Type serviceType)
		{
			return this.container.TryResolve(serviceType);
		}

		/// <summary>
		/// 	Resolves multiply registered services.
		/// </summary>
		/// <returns> The requested services. </returns>
		/// <param name="serviceType"> The type of the requested services. </param>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return this.container.TryResolveAll(serviceType);
		}

		#endregion
	}
}