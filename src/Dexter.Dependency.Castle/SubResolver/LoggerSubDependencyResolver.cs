#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LoggerSubDependencyResolver.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency.Castle.SubResolver
{
	using global::Castle.Core;

	using global::Castle.MicroKernel;

	using global::Castle.MicroKernel.Context;

	using Common.Logging;

	public class LoggerSubDependencyResolver : ISubDependencyResolver
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Returns true if the resolver is able to satisfy this dependency.
		/// </summary>
		/// <param name="context"> Creation context, which is a resolver itself </param>
		/// <param name="contextHandlerResolver"> Parent resolver - normally the IHandler implementation </param>
		/// <param name="model"> Model of the component that is requesting the dependency </param>
		/// <param name="dependency"> The dependency model </param>
		/// <returns> <c>true</c> if the dependency can be satisfied </returns>
		public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
		{
			return dependency.TargetType == typeof(ILog);
		}

		/// <summary>
		/// 	Should return an instance of a service or property values as
		/// 	specified by the dependency model instance. 
		/// 	It is also the responsibility of <see cref="T:Castle.MicroKernel.IDependencyResolver" />
		/// 	to throw an exception in the case a non-optional dependency 
		/// 	could not be resolved.
		/// </summary>
		/// <param name="context"> Creation context, which is a resolver itself </param>
		/// <param name="contextHandlerResolver"> Parent resolver - normally the IHandler implementation </param>
		/// <param name="model"> Model of the component that is requesting the dependency </param>
		/// <param name="dependency"> The dependency model </param>
		/// <returns> The dependency resolved value or null </returns>
		public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
		{
			if (this.CanResolve(context, contextHandlerResolver, model, dependency))
			{
				if (dependency.TargetType == typeof(ILog))
				{
					return LogManager.GetLogger(model.Implementation);
				}
			}

			return null;
		}

		#endregion
	}
}