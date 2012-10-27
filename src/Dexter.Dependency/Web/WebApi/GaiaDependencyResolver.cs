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