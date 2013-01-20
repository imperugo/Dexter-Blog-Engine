#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			GaiaDependencyResolver.cs
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

namespace Dexter.Dependency.Web.WebApi
{
	using System;
	using System.Collections.Generic;
	using System.Web.Http.Dependencies;

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

		public IDependencyScope BeginScope()
		{
			return this;
		}

		/// <summary>
		/// 	Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
		}

		public object GetService(Type serviceType)
		{
			return this.container.TryResolve(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return this.container.TryResolveAll(serviceType);
		}

		#endregion
	}
}