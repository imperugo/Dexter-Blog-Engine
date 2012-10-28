#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ArraySubDependencyResolver.cs
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