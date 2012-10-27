namespace Dexter.Dependency.Castle
{
	using System.Web;

	using global::Castle.MicroKernel.Lifestyle;

	using Dexter.Dependency.Installation;

	public class CastleInstaller : ILayerInstaller
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name="container"> The container. </param>
		public void ApplicationStarted(IDexterContainer container)
		{
		}

		/// <summary>
		/// 	Installs all the component needed by the assembly.
		/// </summary>
		/// <param name="container"> The container. </param>
		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<IHttpModule, PerWebRequestLifestyleModule>(LifeCycle.Singleton);
		}

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name="container"> The container. </param>
		public void ServiceRegistrationComplete(IDexterContainer container)
		{
		}

		#endregion
	}
}