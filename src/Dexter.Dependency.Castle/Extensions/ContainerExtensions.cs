#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ContainerExtensions.cs
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

namespace Castle.Windsor
{
	using System.Linq;

	using Castle.MicroKernel;

	internal static class ContainerExtensions
	{
		#region Public Methods and Operators

		public static bool IsFacilityRegistered<T>(this IWindsorContainer container)
		{
			IFacility[] facilities = container.Kernel.GetFacilities();

			return facilities.Any(facility => facility.GetType() == typeof(T));
		}

		/// <summary>
		/// 	Determines whether the specified service type T is registered in the container kernel.
		/// </summary>
		/// <typeparam name="T"> The type to look for. </typeparam>
		/// <param name="container"> The container. </param>
		/// <returns> <c>true</c> if the specified service type T is registered; otherwise, <c>false</c> . </returns>
		public static bool IsRegistered<T>(this IWindsorContainer container)
		{
			return container.Kernel.HasComponent(typeof(T));
		}

		/// <summary>
		/// 	Determines whether the specified container is registered.
		/// </summary>
		/// <param name="container"> The container. </param>
		/// <param name="name"> The name. </param>
		/// <returns> <c>true</c> if the specified container is registered; otherwise, <c>false</c> . </returns>
		public static bool IsRegistered(this IWindsorContainer container, string name)
		{
			return container.Kernel.HasComponent(name);
		}

		#endregion
	}
}