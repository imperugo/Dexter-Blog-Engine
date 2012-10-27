namespace Dexter.Dependency
{
	using System;

	/// <summary>
	/// 	This is the interface to generate concrete IoC engine.
	/// </summary>
	public interface IDexterContainerFactory
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Creates this instance.
		/// </summary>
		/// <returns> An instance of the Container </returns>
		IDexterContainer Create();

		/// <summary>
		/// 	Creates this instance.
		/// </summary>
		/// <param name="configuration"> The configuration. </param>
		/// <returns> An instance of the Container </returns>
		IDexterContainer Create(String configuration);

		#endregion
	}
}