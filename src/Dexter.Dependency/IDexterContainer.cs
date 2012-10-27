#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IDexterContainer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/DexterBlogEngine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency
{
	using System;
	using System.Reflection;

	/// <summary>
	/// 	Contract for the IoC Framework like Castle, Unity, Spring, etc
	/// </summary>
	public interface IDexterContainer
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Configures the container wirth the specified file path.
		/// </summary>
		/// <param name="filePath"> The filePath. </param>
		void Configure(string filePath);

		/// <summary>
		/// 	Determines whether the specified key has registered for a component.
		/// </summary>
		/// <typeparam name="T"> The type of the registered instance. </typeparam>
		/// <returns> <c>true</c> if the specified key has component; otherwise, <c>false</c> . </returns>
		bool HasComponent<T>();

		/// <summary>
		/// 	Determines whether the specified key has registered for a component.
		/// </summary>
		/// <param name="type"> The type. </param>
		/// <returns> <c>true</c> if the specified key has component; otherwise, <c>false</c> . </returns>
		bool HasComponent(Type type);

		/// <summary>
		/// 	Registers a new <c>type</c> to the container.
		/// </summary>
		/// <param name="type"> The type to register. </param>
		/// <param name="instance"> The instance of <paramref name="type" /> . </param>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		void Register(Type type, object instance, LifeCycle lifeCycle);

		/// <summary>
		/// 	Registers a new <c>type</c> to the container.
		/// </summary>
		/// <param name="type"> The type to register. </param>
		/// <param name="implementedBy"> The implementation of <paramref name="type" /> . </param>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		void Register(Type type, Type implementedBy, LifeCycle lifeCycle);

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
		void Register<T, TK>(TK instance, LifeCycle lifeCycle) where TK : class, T;

		/// <summary>
		/// 	Registers the specified key.
		/// </summary>
		/// <typeparam name="T"> The type to register </typeparam>
		/// <typeparam name="TK"> The implementation of the generic type <c>T</c> . </typeparam>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		void Register<T, TK>(LifeCycle lifeCycle) where TK : class, T;

		/// <summary>
		/// 	Registers the specified type <c>T</c> for all implmentation of <paramref name="assembly" />.
		/// </summary>
		/// <typeparam name="T"> The type to register </typeparam>
		/// <param name="assembly"> The assembly. </param>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		void Register<T>(Assembly assembly, LifeCycle lifeCycle);

		/// <summary>
		/// 	Registers the specified type <c>T</c> for all implmentation of <paramref name="assembly" />.
		/// </summary>
		/// <typeparam name="T"> The type to register </typeparam>
		/// <param name="assembly"> The assembly. </param>
		/// <param name="lifeCycle"> The life cycle. </param>
		/// <remarks>
		/// 	Singleton should be the default lifecycle.
		/// </remarks>
		void RegisterComponentsByBaseClass<T>(Assembly assembly, LifeCycle lifeCycle);

		/// <summary>
		/// 	Releases the specified instance from the container.
		/// </summary>
		/// <param name="instance"> The instance. </param>
		void Release(object instance);

		/// <summary>
		/// 	Retrieve the instance for the specified <c>type</c>.
		/// </summary>
		/// <param name="type"> The type. </param>
		/// <returns> The registered instance or null if the instance will not fund. </returns>
		object Resolve(Type type);

		/// <summary>
		/// 	Retrieve the instance for the specified <c>type {T}</c>.
		/// </summary>
		/// <typeparam name="T"> The type of the registered instance. </typeparam>
		/// <returns> The registered instance or null if the instance will not fund. </returns>
		T Resolve<T>();

		/// <summary>
		/// 	Retrieve all registered instances for the specified <c>type</c>.
		/// </summary>
		/// <typeparam name="T"> The registered type. </typeparam>
		/// <returns> Null if there aren't registered instances for the specified <c>type</c> . Otherwise an array of the generic type <c>T</c> . </returns>
		T[] ResolveAll<T>();

		/// <summary>
		/// 	Retrieve all registered instances for the specified
		/// 	<param name="type" />
		/// 	.
		/// </summary>
		/// <returns> Null if there aren't registered instances for the specified <c>type</c> . Otherwise an array of <c>object</c> . </returns>
		object[] ResolveAll(Type type);

		/// <summary>
		/// 	Dispose the container.
		/// </summary>
		void Shutdown();

		///<summary>
		///	Retrieve the instance for the specified <c>type</c>.
		///</summary>
		///<remarks>
		///	If there isn't components for the specified type, the container will return null.
		///</remarks>
		///<param name="type"> The type. </param>
		///<returns> The registered instance or null if the instance will not fund. </returns>
		object TryResolve(Type type);

		///<summary>
		///	Retrieve the instance for the specified <c>type {T}</c>.
		///</summary>
		///<remarks>
		///	If there isn't components for the specified type, the container will return null.
		///</remarks>
		///<typeparam name="T"> The type of the registered instance. </typeparam>
		///<returns> The registered instance or null if the instance will not fund. </returns>
		T TryResolve<T>();

		///<summary>
		///	Retrieve all registered instances for the specified <c>type</c>.
		///</summary>
		///<remarks>
		///	If there isn't components for the specified type, the container will return null.
		///</remarks>
		///<typeparam name="T"> The registered type. </typeparam>
		///<returns> Null if there aren't registered instances for the specified <c>type</c> . Otherwise an array of the generic type <c>T</c> . </returns>
		T[] TryResolveAll<T>();

		/// <summary>
		/// 	Retrieve all registered instances for the specified
		/// 	<param name="type" />
		/// 	.
		/// </summary>
		/// <returns> Null if there aren't registered instances for the specified <c>type</c> . Otherwise an array of <c>object</c> . </returns>
		object[] TryResolveAll(Type type);

		#endregion
	}
}