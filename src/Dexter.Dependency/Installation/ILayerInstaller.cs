namespace Dexter.Dependency.Installation {
	/// <summary>
	/// The interface used for component registration
	/// </summary>
	public interface ILayerInstaller {
		/// <summary>
		/// 	Installs all the component needed by the assembly.
		/// </summary>
		/// <param name = "container">The container.</param>
		void ServiceRegistration ( IDexterContainer container );

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name = "container">The container.</param>
		void ServiceRegistrationComplete ( IDexterContainer container );

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name = "container">The container.</param>
		void ApplicationStarted ( IDexterContainer container );
	}
}