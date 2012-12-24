#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ICallContext.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Async
{
	/// <summary>
	/// Th contract for the base call context
	/// </summary>
	public interface ICallContext
	{
		#region Public Indexers

		/// <summary>
		/// 	Gets the items.
		/// </summary>
		/// <value>The items.</value>
		object this[string key] { get; set; }

		#endregion
	}
}