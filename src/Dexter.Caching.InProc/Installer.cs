namespace Dexter.Caching.InProc
{
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;

	public class Installer : ILayerInstaller
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
			container.Register<ICacheProvider, InProcCacheProvider>(LifeCycle.PerWebRequest);
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