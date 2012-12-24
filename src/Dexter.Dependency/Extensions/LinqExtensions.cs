#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LinqExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency.Extensions
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// 	Adds behavior to System.Linq.
	/// </summary>
	public static class LinqExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Eaches the specified enumeration.
		/// </summary>
		/// <typeparam name="T"> </typeparam>
		/// <param name="source"> The enumeration. </param>
		/// <param name="action"> The action. </param>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T item in source)
			{
				action(item);
			}
		}

		#endregion
	}
}