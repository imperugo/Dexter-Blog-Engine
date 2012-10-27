namespace Dexter.Dependency
{
	using System;
	using System.Collections.ObjectModel;
	using System.Configuration;
	using System.Linq;
	using System.Reflection;
	using System.Web.Compilation;

	using Dexter.Dependency.Extensions;
	using Dexter.Dependency.Installation;

	public static class DexterContainer
	{
		#region Static Fields

		private static bool started;

		#endregion

		#region Public Properties

		/// <summary>
		/// 	Gets the engine.
		/// </summary>
		/// <value> The engine. </value>
		public static IDexterContainer Engine { get; private set; }

		#endregion

		#region Public Methods and Operators

		public static void Configure(string filePath)
		{
			Engine.Configure(filePath);
		}

		public static bool HasComponent<T>()
		{
			return Engine.HasComponent<T>();
		}

		public static bool HasComponent(Type type)
		{
			return Engine.HasComponent(type);
		}

		public static void Register(Type type, object instance, LifeCycle lifeCycle)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}

			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			Engine.Register(type, instance, lifeCycle);
		}

		public static void Register(Type type, Type implementedBy, LifeCycle lifeCycle)
		{
			if (implementedBy == null)
			{
				throw new ArgumentNullException("implementedBy");
			}

			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			Engine.Register(type, implementedBy, lifeCycle);
		}

		public static void Register<T, TK>(TK instance, LifeCycle lifeCycle) where TK : class, T
		{
			Engine.Register<T, TK>(instance, lifeCycle);
		}

		public static void Register<T, TK>(LifeCycle lifeCycle) where TK : class, T
		{
			Engine.Register<T, TK>(lifeCycle);
		}

		public static void Register<T>(Assembly assembly, LifeCycle lifeCycle)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}

			Engine.Register<T>(assembly, lifeCycle);
		}

		public static void RegisterComponentsByBaseClass<T>(Assembly assembly, LifeCycle lifeCycle)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}

			Engine.RegisterComponentsByBaseClass<T>(assembly, lifeCycle);
		}

		public static void Release(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}

			Engine.Release(instance);
		}

		public static object Resolve(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			return Engine.Resolve(type);
		}

		public static T Resolve<T>()
		{
			return Engine.Resolve<T>();
		}

		public static T[] ResolveAll<T>()
		{
			return Engine.ResolveAll<T>();
		}

		public static object[] ResolveAll(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			return Engine.ResolveAll(type);
		}

		public static void SetCurrent(IDexterContainer newEngine)
		{
			if (Engine != null)
			{
				Engine.Shutdown();
			}

			Engine = newEngine;
		}

		/// <summary>
		/// 	Starts up.
		/// </summary>
		public static void StartUp()
		{
			if (started)
			{
				return;
			}

			ReadOnlyCollection<Assembly> alpAssemblies = new ReadOnlyCollection<Assembly>(BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(x => x.FullName.StartsWith("Dexter")).ToList());

			Type containerType = null;

			foreach (Assembly a in alpAssemblies)
			{
				foreach (Type t in a.GetTypes())
				{
					if (!t.IsInterface && !t.IsAbstract && typeof(IDexterContainerFactory).IsAssignableFrom(t))
					{
						containerType = t;
					}
				}
			}

			IDexterContainerFactory factory = Activator.CreateInstance(containerType) as IDexterContainerFactory;

			if (factory == null)
			{
				throw new ConfigurationErrorsException(string.Format("The type {0} does not implement the IDexterContainerFactory interface.", containerType.FullName));
			}

			Engine = factory.Create();
			Engine.Register(typeof(IDexterContainer), Engine, LifeCycle.Singleton);

			alpAssemblies.ForEach(x => Engine.Register<ILayerInstaller>(x, LifeCycle.Singleton));

			ILayerInstaller[] coreInstaller = Engine.ResolveAll<ILayerInstaller>();
			coreInstaller.ForEach(x => x.ServiceRegistration(Engine));
			coreInstaller.ForEach(x => x.ServiceRegistrationComplete(Engine));
			coreInstaller.ForEach(x => x.ApplicationStarted(Engine));

			started = true;
		}

		public static object TryResolve(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			return Engine.TryResolve(type);
		}

		public static T TryResolve<T>()
		{
			return Engine.TryResolve<T>();
		}

		public static T[] TryResolveAll<T>()
		{
			return Engine.TryResolveAll<T>();
		}

		public static object[] TryResolveAll(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			return Engine.TryResolveAll(type);
		}

		#endregion
	}
}