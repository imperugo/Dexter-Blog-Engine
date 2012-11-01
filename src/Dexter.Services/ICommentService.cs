#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ICommentService.cs
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

namespace Dexter.Services
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Services.Events;

	public interface ICommentService
	{
		#region Public Events

		/// <summary>
		/// This event will raise after to retrieve a list of <see cref="CommentDto"/> by specific item id. The event is raised by by the implementation of <see cref="GetCommentForSpecificItem"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IList<CommentDto>>> CommentsRetrievedByItemId;

		/// <summary>
		/// This event will raise before to retrieve a list of <see cref="CommentDto"/> by specific item id. The event is raised by by the implementation of <see cref="GetCommentForSpecificItem"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<int, IList<CommentDto>>> CommentsRetrievingByItemId;

		/// <summary>
		/// This event will raise after to retrieve a list of recent <see cref="CommentDto"/>. The event is raised by by the implementation of <see cref="GetRecentComments"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IList<CommentDto>>> RecentCommentsRetrieved;

		/// <summary>
		/// This event will raise before to retrieve a list of recent <see cref="CommentDto"/>. The event is raised by by the implementation of <see cref="GetRecentComments"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<int, IList<CommentDto>>> RecentRetrieving;

		#endregion

		#region Public Methods and Operators

		IList<CommentDto> GetCommentForSpecificItem(int itemId, CommentQueryFilter queryFilter = null);

		Task<IList<CommentDto>> GetCommentForSpecificItemAsync(int itemId, CommentQueryFilter queryFilter = null);

		IList<CommentDto> GetRecentComments(int maxNumber, CommentQueryFilter queryFilter = null);

		Task<IList<CommentDto>> GetRecentCommentsAsync(int maxNumber, CommentQueryFilter queryFilter = null);

		#endregion
	}
}