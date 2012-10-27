namespace Castle.Windsor
{
	using System;
	using System.Linq;

	using Castle.MicroKernel;

	internal static class ContainerExtensions
	{
		#region Public Methods and Operators

		public static Boolean IsFacilityRegistered<T>(this IWindsorContainer container)
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
		public static Boolean IsRegistered<T>(this IWindsorContainer container)
		{
			return container.Kernel.HasComponent(typeof(T));
		}

		/// <summary>
		/// 	Determines whether the specified container is registered.
		/// </summary>
		/// <param name="container"> The container. </param>
		/// <param name="name"> The name. </param>
		/// <returns> <c>true</c> if the specified container is registered; otherwise, <c>false</c> . </returns>
		public static Boolean IsRegistered(this IWindsorContainer container, string name)
		{
			return container.Kernel.HasComponent(name);
		}

		#endregion
	}
}