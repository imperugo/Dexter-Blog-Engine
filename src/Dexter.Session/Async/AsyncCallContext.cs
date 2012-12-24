#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AsyncCallContext.cs
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

namespace Dexter.Async.Async
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// This class is the implementation of ICallContext
	/// </summary>
	public class AsyncCallContext : IAsyncCallContext
	{
		#region Static Fields

		[ThreadStatic]
		private static Dictionary<string, object> customTls;

		#endregion

		#region Properties

		private static Dictionary<string, object> CustomTls
		{
			get
			{
				return customTls ?? (customTls = new Dictionary<string, object>());
			}
		}

		#endregion

		#region Public Indexers

		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <value>The items.</value>
		public object this[string key]
		{
			get
			{
				object retvalue;
				if (!CustomTls.TryGetValue(key, out retvalue))
				{
					return null;
				}

				return retvalue;
			}

			set
			{
				CustomTls[key] = value;
			}
		}

		#endregion
	}
}