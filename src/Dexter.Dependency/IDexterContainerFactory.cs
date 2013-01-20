#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IDexterContainerFactory.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/27
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency
{
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
		IDexterContainer Create(string configuration);

		#endregion
	}
}