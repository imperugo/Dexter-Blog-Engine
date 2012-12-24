#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PagedResult.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Entities.Result
{
	using System.Collections.Generic;

	/// <summary>
	/// 	The implementation of <see cref="IPagedResult{T}" />
	/// </summary>
	/// <typeparam name="T"> </typeparam>
	public class PagedResult<T> : IPagedResult<T>
	{
		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="PagedResult&lt;T&gt;" /> class.
		/// </summary>
		/// <param name="pageIndex"> Index of the page. </param>
		/// <param name="pageSize"> Size of the page. </param>
		/// <param name="result"> The result. </param>
		/// <param name="totalCount"> The total count. </param>
		public PagedResult(int pageIndex, int pageSize, IEnumerable<T> result, long totalCount)
		{
			this.PageIndex = pageIndex;
			this.PageSize = pageSize;
			this.Result = result;
			this.TotalCount = totalCount;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// 	Gets a value indicating whether this instance has next page.
		/// </summary>
		/// <value> <c>true</c> if this instance has next page; otherwise, <c>false</c> . </value>
		public bool HasNextPage
		{
			get
			{
				return (this.PageIndex * this.PageSize) <= this.TotalCount;
			}
		}

		/// <summary>
		/// 	Gets a value indicating whether this instance has previous page.
		/// </summary>
		/// <value> <c>true</c> if this instance has previous page; otherwise, <c>false</c> . </value>
		public bool HasPreviousPage
		{
			get
			{
				return this.PageIndex > 0;
			}
		}

		/// <summary>
		/// 	Gets or sets the index of the page.
		/// </summary>
		/// <value> The index of the page. </value>
		public int PageIndex { get; private set; }

		/// <summary>
		/// 	Gets or sets the size of the page.
		/// </summary>
		/// <value> The size of the page. </value>
		public int PageSize { get; private set; }

		/// <summary>
		/// 	Gets or sets the result.
		/// </summary>
		/// <value> The result. </value>
		public IEnumerable<T> Result { get; private set; }

		/// <summary>
		/// 	Gets or sets the total count.
		/// </summary>
		/// <value> The total count. </value>
		public long TotalCount { get; private set; }

		/// <summary>
		/// 	Gets the total pages.
		/// </summary>
		/// <value> The total pages. </value>
		public int TotalPages
		{
			get
			{
				return (int)(this.TotalCount / this.PageSize) + (this.TotalCount % this.PageSize == 0 ? 0 : 1);
			}
		}

		#endregion
	}
}