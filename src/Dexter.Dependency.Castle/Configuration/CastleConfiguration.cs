namespace Dexter.Dependency.Castle.Configuration
{
	using global::Castle.DynamicProxy;

	using global::Castle.MicroKernel.Registration;

	using global::Castle.Windsor;

	/// <summary>
	/// 	This class contains the Windsor configuration
	/// </summary>
	internal static class CastleConfiguration
	{
		#region Public Methods and Operators

		public static void RegisterInterceptor(IWindsorContainer container)
		{
			container.Register(AllTypes.FromAssembly(typeof(CastleConfiguration).Assembly).BasedOn<IInterceptor>().Configure(reg => reg.LifeStyle.Singleton.Named(reg.Implementation.Name)).WithService.FromInterface());
		}

		#endregion
	}
}