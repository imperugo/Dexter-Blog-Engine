#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPagedResult (generic).cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/28
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Entities.Result
{
	using System.Collections.Generic;

	/// <summary>
	/// 	This is the base contract for the return objects of paging methods
	/// </summary>
	/// <typeparam name="T"> </typeparam>
	public interface IPagedResult<out T>
	{
		#region Public Properties

		/// <summary>
		/// 	Gets a value indicating whether this instance has next page.
		/// </summary>
		/// <value> <c>true</c> if this instance has next page; otherwise, <c>false</c> . </value>
		bool HasNextPage { get; }

		/// <summary>
		/// 	Gets a value indicating whether this instance has previous page.
		/// </summary>
		/// <value> <c>true</c> if this instance has previous page; otherwise, <c>false</c> . </value>
		bool HasPreviousPage { get; }

		/// <summary>
		/// 	Gets the index of the page.
		/// </summary>
		/// <value> The index of the page. </value>
		int PageIndex { get; }

		/// <summary>
		/// 	Gets the size of the page.
		/// </summary>
		/// <value> The size of the page. </value>
		int PageSize { get; }

		/// <summary>
		/// 	Gets the result.
		/// </summary>
		/// <value> The result. </value>
		IEnumerable<T> Result { get; }

		/// <summary>
		/// 	Gets the total count.
		/// </summary>
		/// <value> The total count. </value>
		long TotalCount { get; }

		/// <summary>
		/// 	Gets the total pages.
		/// </summary>
		/// <value> The total pages. </value>
		int TotalPages { get; }

		#endregion
	}
}