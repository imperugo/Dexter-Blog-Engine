#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LinqExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/20
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace System.Linq
{
	using System.Collections.Generic;

	public static class LinqExtensions
	{
		#region Public Methods and Operators

		public static T GetRandom<T>(this IEnumerable<T> list)
		{
			return list.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
		}

		/// <summary>
		/// 	Toes the flat.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "source">The source.</param>
		/// <param name = "childPredicate">The child predicate.</param>
		/// <returns></returns>
		public static IEnumerable<T> ToFlat<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childPredicate)
		{
			foreach (T t in source)
			{
				yield return t;
				IEnumerable<T> children = childPredicate(t);
				
				if (children != null)
				{
					foreach (T child in ToFlat(children, childPredicate))
					{
						yield return child;
					}
				}
			}
		}

		#endregion
	}
}