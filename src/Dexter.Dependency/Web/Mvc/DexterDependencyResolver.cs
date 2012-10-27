using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Dexter.Dependency.Web.Mvc
{
	public class DexterDependencyResolver : IDependencyResolver
	{
		readonly IDexterContainer container;

		public DexterDependencyResolver(IDexterContainer container)
		{
			this.container = container;
		}

		#region Implementation of IDependencyResolver

		/// <summary>
		/// Resolves singly registered services that support arbitrary object creation.
		/// </summary>
		/// <returns>
		/// The requested service or object.
		/// </returns>
		/// <param name="serviceType">The type of the requested service or object.</param>
		public object GetService(Type serviceType)
		{
			return container.TryResolve(serviceType);
		}

		/// <summary>
		/// Resolves multiply registered services.
		/// </summary>
		/// <returns>
		/// The requested services.
		/// </returns>
		/// <param name="serviceType">The type of the requested services.</param>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return container.TryResolveAll(serviceType);
		}

		#endregion
	}
}