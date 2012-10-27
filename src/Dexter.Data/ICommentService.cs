#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ICommentService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data
{
	using Dexter.Data.DataTransferObjects;
	using Dexter.Data.DataTransferObjects.Result;

	public interface ICommentService
	{
		#region Public Methods and Operators

		PagedResult<CommentDto> GetCommentForSpecificItem(int postId, int pageIndex, int pageSize, CommentQueryFilter queryFilter);

		#endregion
	}

	public class CommentQueryFilter
	{
		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Object" /> class.
		/// </summary>
		public CommentQueryFilter()
		{
			this.CommentStatus = CommentStatus.IsApproved;
		}

		#endregion

		#region Public Properties

		public CommentStatus CommentStatus { get; set; }

		#endregion
	}
}