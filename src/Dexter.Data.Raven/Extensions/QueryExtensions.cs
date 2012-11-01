#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			QueryExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Extensions
{
	using System.Linq;

	internal static class QueryExtensions
	{
		#region Public Methods and Operators

		public static IQueryable<T> Paging<T>(this IQueryable<T> query, int currentPage, int pageSize)
		{
			return query
				.Skip((currentPage - 1) * pageSize)
				.Take(pageSize);
		}

		#endregion
	}
}