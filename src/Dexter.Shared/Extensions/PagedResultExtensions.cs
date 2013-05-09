#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PagedResultExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/27
// Last edit:	2013/04/27
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Shared.Extensions
{
	using System.Collections.Generic;

	using Dexter.Shared.Result;

	public static class PagedResultExtensions
	{
		#region Public Methods and Operators

		public static IPagedResult<T> ToPagedResult<T>(this IEnumerable<T> items, int pageIndex, int pageSize, long totalCount)
		{
			return new PagedResult<T>(pageIndex, pageSize, items, totalCount);
		}

		#endregion
	}
}