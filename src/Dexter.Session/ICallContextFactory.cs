#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ICallContextFactory.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/02
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Async
{
	/// <summary>
	/// This class resolve the context calls implementations
	/// </summary>
	public interface ICallContextFactory
	{
		#region Public Properties

		/// <summary>
		/// Determines whether [is web request].
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if [is web request]; otherwise, <c>false</c>.
		/// </returns>
		bool IsWebRequest { get; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Return the WebCallContext or the ThreadCallContext
		/// </summary>
		/// <returns></returns>
		ICallContext RetrieveCallContext();

		#endregion
	}
}