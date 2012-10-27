#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LifeCycle.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency
{
	/// <summary>
	/// 	The lifecycle of the component registration.
	/// </summary>
	public enum LifeCycle
	{
		/// <summary>
		/// 	A new instance will be released for each request
		/// </summary>
		Transient, 

		/// <summary>
		/// 	The same instance will be released for each web request.
		/// </summary>
		PerWebRequest, 

		/// <summary>
		/// 	The same instance will be released for the application lifecycle (static container).
		/// </summary>
		Singleton, 

		/// <summary>
		/// 	The same instance will be released for each thread.
		/// </summary>
		Thread
	}
}