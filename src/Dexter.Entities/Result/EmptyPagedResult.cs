#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			EmptyPagedResult.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Entities.Result
{
	/// <summary>
	/// 	The implementation of <see cref="IPagedResult{T}" /> used for empty result;
	/// </summary>
	/// <typeparam name="T"> </typeparam>
	public class EmptyPagedResult<T> : PagedResult<T>
	{
		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="EmptyPagedResult{T}" /> class.
		/// </summary>
		/// <param name="pageIndex"> Index of the page. </param>
		/// <param name="pageSize"> Size of the page. </param>
		public EmptyPagedResult(int pageIndex, int pageSize)
			: base(pageIndex, pageSize, null, 0)
		{
		}

		#endregion
	}
}