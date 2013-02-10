#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AsyncExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/02/01
// Last edit:	2013/02/01
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Extensions.Async
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public static class AsyncExtensions
	{
		#region Public Methods and Operators

		public static async Task ForEachAsync<T>(IEnumerable<T> items, Func<T, Task> func)
		{
			foreach (T item in items)
			{
				await func(item);
			}
		}

		#endregion
	}
}