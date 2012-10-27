namespace Dexter.Dependency {
	/// <summary>
	/// 	The lifecycle of the component registratin.
	/// </summary>
	public enum LifeCycle {
		/// <summary>
		/// 	A new instance will be released for each request
		/// </summary>
		Transient,

		/// <summary>
		/// 	The same instance will be released for each web request (It use an httpmodule)
		/// </summary>
		PerWebRequest,

		/// <summary>
		/// 	The same instance will be released for the application lifecycle (static container)
		/// </summary>
		Singleton,

		/// <summary>
		/// 	The same instance will be released for each thread
		/// </summary>
		Thread
	}
}