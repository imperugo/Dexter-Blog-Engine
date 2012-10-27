using System;

namespace Dexter.Dependency {
	/// <summary>
	/// 	This is the interface to generate concrete IoC engine.
	/// </summary>
	public interface IGaiaContainerFactory {
		/// <summary>
		/// 	Creates this instance.
		/// </summary>
		/// <returns>An instance of the Container</returns>
		IGaiaContainer Create ();

		/// <summary>
		/// 	Creates this instance.
		/// </summary>
		/// <param name = "configuration">The configuration.</param>
		/// <returns>An instance of the Container</returns>
		IGaiaContainer Create ( String configuration );
	}
}