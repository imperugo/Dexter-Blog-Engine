using System;
using System.Collections.Generic;

namespace Dexter.Dependency.Extensions {

	/// <summary>
	/// 	Adds behavior to System.Linq.
	/// </summary>
	public static class LinqExtensions {
		/// <summary>
		/// 	Eaches the specified enumeration.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "source">The enumeration.</param>
		/// <param name = "action">The action.</param>
		public static void ForEach <T> ( this IEnumerable <T> source , Action <T> action ) {
			foreach ( T item in source ) {
				action ( item );
			}
		}
	}
}