namespace Dexter.Dependency.Castle.SubResolver
{
	using System;
	using System.Collections;

	using global::Castle.Core;

	using global::Castle.MicroKernel;

	using global::Castle.MicroKernel.Context;

	public class ArraySubDependencyResolver : ISubDependencyResolver
	{
		#region Fields

		private readonly IKernel kernel;

		#endregion

		#region Constructors and Destructors

		public ArraySubDependencyResolver(IKernel kernel)
		{
			this.kernel = kernel;
		}

		#endregion

		#region Public Methods and Operators

		public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
		{
			return typeof(IEnumerable).IsAssignableFrom(dependency.TargetType) && dependency.TargetType != typeof(string);
		}

		public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
		{
			if (this.CanResolve(context, contextHandlerResolver, model, dependency))
			{
				if (dependency.TargetType.IsArray)
				{
					Type resolveType = dependency.TargetType.GetElementType();

					return this.kernel.ResolveAll(resolveType);
				}

				if (dependency.TargetType.IsGenericType)
				{
					return this.kernel.ResolveAll(dependency.TargetType.GetGenericArguments()[0]);
				}
			}
			return null;
		}

		#endregion
	}
}