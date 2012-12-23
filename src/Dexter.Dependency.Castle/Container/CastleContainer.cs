#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CastleContainer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/12/23
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Dependency.Castle.Container
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Reflection;

	using global::Castle.Core;

	using global::Castle.MicroKernel.Registration;

	using global::Castle.Windsor;

	using global::Castle.Windsor.Installer;

	using Dexter.Dependency.Castle.Configuration;
	using Dexter.Dependency.Castle.SubResolver;

	/// <summary>
	/// 	The implementation of <see cref="IDexterContainer" /> using <c>Caslte Windsor</c> container <seealso
	/// 	 cref="http://www.castleproject.org/container/" />. />.
	/// </summary>
	public class CastleContainer : IDexterContainer
	{
		#region Fields

		private IWindsorContainer container;

		private IWindsorContainer overrideContainer;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="CastleContainer" /> class.
		/// </summary>
		public CastleContainer()
		{
			this.Container = new WindsorContainer();
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref="CastleContainer" /> class.
		/// </summary>
		/// <param name="container"> The container. </param>
		public CastleContainer(IWindsorContainer container)
		{
			this.Container = container;
		}

		#endregion

		#region Public Properties

		public IWindsorContainer Container
		{
			get
			{
				return this.overrideContainer ?? this.container;
			}

			set
			{
				this.container = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Configures the container wirth the specified file path.
		/// </summary>
		/// <param name="filePath"> The filePath. </param>
		public void Configure(string filePath)
		{
			if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
			{
				this.Container.Install(Configuration.FromXmlFile(filePath));
			}
			else
			{
				this.Container.Install();
			}

			this.container.Kernel.Resolver.AddSubResolver(new ArraySubDependencyResolver(this.container.Kernel));
			this.container.Kernel.Resolver.AddSubResolver(new LoggerSubDependencyResolver());
			CastleConfiguration.RegisterInterceptor(this.Container);
		}

		/// <summary>
		/// 	Determines whether the specified key has registered for a component.
		/// </summary>
		/// <typeparam name="T"> The type of the registered instance. </typeparam>
		/// <returns> <c>true</c> if the specified key has component; otherwise, <c>false</c> . </returns>
		public bool HasComponent<T>()
		{
			return this.Container.Kernel.HasComponent(typeof(T));
		}

		/// <summary>
		/// 	Determines whether the specified key has registered for a component.
		/// </summary>
		/// <param name="type"> The type. </param>
		/// <returns> <c>true</c> if the specified key has component; otherwise, <c>false</c> . </returns>
		public bool HasComponent(Type type)
		{
			return this.Container.Kernel.HasComponent(type);
		}

		/// <summary>
		/// 	Registers a new <c>type</c> to the container.
		/// </summary>
		/// <param name="type"> The type to register. </param>
		/// <param name="instance"> The instance of <paramref name="type" /> . </param>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		public void Register(Type type, object instance, LifeCycle lifeCycle)
		{
			ComponentRegistration<object> component;
			switch (lifeCycle)
			{
				case LifeCycle.PerWebRequest:
					component = Component.For(type).Instance(instance).LifeStyle.Is(LifestyleType.PerWebRequest);
					break;
				case LifeCycle.Transient:
					component = Component.For(type).Instance(instance).LifeStyle.Is(LifestyleType.Transient);
					break;
				case LifeCycle.Thread:
					component = Component.For(type).Instance(instance).LifeStyle.Is(LifestyleType.Thread);
					break;
				default:
					component = Component.For(type).Instance(instance).LifeStyle.Is(LifestyleType.Singleton);
					break;
			}

			this.Container.Register(component);
		}

		/// <summary>
		/// 	Registers a new <c>type</c> to the container.
		/// </summary>
		/// <param name="type"> The type to register. </param>
		/// <param name="implementedBy"> The implementation of <paramref name="type" /> . </param>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		public void Register(Type type, Type implementedBy, LifeCycle lifeCycle)
		{
			ComponentRegistration<object> component;
			switch (lifeCycle)
			{
				case LifeCycle.PerWebRequest:
					component = Component.For(type).ImplementedBy(implementedBy).LifeStyle.Is(LifestyleType.PerWebRequest);
					break;
				case LifeCycle.Transient:
					component = Component.For(type).ImplementedBy(implementedBy).LifeStyle.Is(LifestyleType.Transient);
					break;
				case LifeCycle.Thread:
					component = Component.For(type).ImplementedBy(implementedBy).LifeStyle.Is(LifestyleType.Thread);
					break;
				default:
					component = Component.For(type).ImplementedBy(implementedBy).LifeStyle.Is(LifestyleType.Singleton);
					break;
			}

			this.Container.Register(component);
		}

		/// <summary>
		/// 	Registers the specified key.
		/// </summary>
		/// <typeparam name="T"> The type to register </typeparam>
		/// <typeparam name="TK"> The implementation of the generic type <c>T</c> . </typeparam>
		/// <param name="instance"> The instance of the generic type <c>TK</c> . </param>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		public void Register<T, TK>(TK instance, LifeCycle lifeCycle) where TK : class, T
		{
			Register(typeof(T), instance, lifeCycle);
		}

		/// <summary>
		/// 	Registers the specified key.
		/// </summary>
		/// <typeparam name="T"> The type to register </typeparam>
		/// <typeparam name="TK"> The implementation of the generic type <c>T</c> . </typeparam>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		public void Register<T, TK>(LifeCycle lifeCycle) where TK : class, T
		{
			this.Register(typeof(T), typeof(TK), lifeCycle);
		}

		/// <summary>
		/// 	Registers the specified type <c>T</c> for all implementation of <paramref name="assembly" />.
		/// </summary>
		/// <typeparam name="T"> The type to register </typeparam>
		/// <param name="assembly"> The assembly. </param>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		public void Register<T>(Assembly assembly, LifeCycle lifeCycle)
		{
			BasedOnDescriptor set = Classes.FromAssembly(assembly).BasedOn<T>().WithServiceAllInterfaces().Unless(cd => cd.IsAbstract);

			switch (lifeCycle)
			{
				case LifeCycle.PerWebRequest:
					set.Configure(cd => cd.LifeStyle.Is(LifestyleType.PerWebRequest));
					break;
				case LifeCycle.Transient:
					set.Configure(cd => cd.LifeStyle.Is(LifestyleType.Transient));
					break;
				case LifeCycle.Thread:
					set.Configure(cd => cd.LifeStyle.Is(LifestyleType.Thread));
					break;
				default:
					set.Configure(cd => cd.LifeStyle.Is(LifestyleType.Singleton));
					break;
			}

			this.Container.Register(set);
		}

		/// <summary>
		/// 	Registers the specified type <c>T</c> for all implementation of <paramref name="assembly" />.
		/// </summary>
		/// <typeparam name="T"> The type to register </typeparam>
		/// <param name="assembly"> The assembly. </param>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		public void RegisterComponentsByBaseClass<T>(Assembly assembly, LifeCycle lifeCycle)
		{
			Type baseClass = typeof(T);

			IEnumerable<Type> controllers = assembly.GetTypes().Where(c => !c.IsAbstract && baseClass.IsAssignableFrom(c));

			foreach (Type controller in controllers)
			{
				if (controller == null || string.IsNullOrEmpty(controller.FullName))
				{
					continue;
				}

				Register(controller, controller, lifeCycle);
			}
		}

		/// <summary>
		/// 	Releases the specified instance from the container.
		/// </summary>
		/// <param name="instance"> The instance. </param>
		public void Release(object instance)
		{
			this.Container.Kernel.ReleaseComponent(instance);
		}

		/// <summary>
		/// 	Retrieve the instance for the specified <c>type</c>.
		/// </summary>
		/// <param name="type"> The type. </param>
		/// <returns> The registered instance or null if the instance will not fund. </returns>
		public object Resolve(Type type)
		{
			return this.Container.Resolve(type);
		}

		/// <summary>
		/// 	Retrieve the instance for the specified <c>type {T}</c>.
		/// </summary>
		/// <typeparam name="T"> The type of the registered instance. </typeparam>
		/// <returns> The registered instance or null if the instance will not fund. </returns>
		public T Resolve<T>()
		{
			return this.Container.Resolve<T>();
		}

		/// <summary>
		/// 	Retrieve all registered instances for the specified <c>type</c>.
		/// </summary>
		/// <typeparam name="T"> The registered type. </typeparam>
		/// <returns> Null if there aren't registered instances for the specified <c>type</c> . Otherwise an array of the generic type <c>T</c> . </returns>
		public T[] ResolveAll<T>()
		{
			return this.Container.ResolveAll<T>();
		}

		/// <summary>
		/// 	Retrieve all registered instances for the specified
		/// 	<param name="type" />
		/// 	.
		/// </summary>
		/// <returns> Null if there aren't registered instances for the specified <c>type</c> . Otherwise an array of <c>object</c> . </returns>
		public object[] ResolveAll(Type type)
		{
			return (object[])this.Container.ResolveAll(type);
		}

		/// <summary>
		/// 	Dispose the container.
		/// </summary>
		public void Shutdown()
		{
			if (this.Container != null)
			{
				this.Container.Dispose();
				this.Container = null;
			}
		}

		/// <summary>
		///	Retrieve the instance for the specified <c>type</c>.
		/// </summary>
		/// <remarks>
		///	If there isn't components for the specified type, the container will return null.
		/// </remarks>
		/// <param name="type"> The type. </param>
		/// <returns> The registered instance or null if the instance will not fund. </returns>
		public object TryResolve(Type type)
		{
			return this.Container.Kernel.HasComponent(type) ? this.Container.Resolve(type) : null;
		}

		/// <summary>
		///	Retrieve the instance for the specified <c>type {T}</c>.
		/// </summary>
		/// <remarks>
		///	If there isn't components for the specified type, the container will return null.
		/// </remarks>
		/// <typeparam name="T"> The type of the registered instance. </typeparam>
		/// <returns> The registered instance or null if the instance will not fund. </returns>
		public T TryResolve<T>()
		{
			return this.HasComponent<T>() ? this.Container.Resolve<T>() : default(T);
		}

		/// <summary>
		///	Retrieve all registered instances for the specified <c>type</c>.
		/// </summary>
		/// <remarks>
		///	If there isn't components for the specified type, the container will return null.
		/// </remarks>
		/// <typeparam name="T"> The registered type. </typeparam>
		/// <returns> Null if there aren't registered instances for the specified <c>type</c> . Otherwise an array of the generic type <c>T</c> . </returns>
		public T[] TryResolveAll<T>()
		{
			return this.Container.ResolveAll<T>();
		}

		/// <summary>
		/// 	Retrieve all registered instances for the specified
		/// 	<param name="type" />.
		/// </summary>
		/// <returns> Null if there aren't registered instances for the specified <c>type</c> . Otherwise an array of <c>object</c> . </returns>
		public object[] TryResolveAll(Type type)
		{
			return (object[])this.Container.ResolveAll(type);
		}

		#endregion

		#region Methods

		internal IDisposable OverrideContainer(IWindsorContainer windsorContainer)
		{
			this.overrideContainer = windsorContainer;
			return new DisposableAction(() => this.overrideContainer = null);
		}

		#endregion
	}
}