#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CommentDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven
{
	using System;
	using System.Collections.Generic;

	using Common.Logging;

	using Dexter.Data.Exceptions;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Extensions;
	using Dexter.Entities;

	using global::Raven.Client;

	public class CommentDataService : ServiceBase, ICommentDataService
	{
		#region Constructors and Destructors

		public CommentDataService(ILog logger, IDocumentSession session)
			: base(logger, session)
		{
		}

		#endregion

		#region Public Methods and Operators

		public void AddComment(CommentDto comment, int itemId, CommentStatus status)
		{
			Item item = this.Session.Load<Item>(itemId);

			if (item == null)
			{
				throw new ItemNotFoundException("id");
			}

			ItemComments itemComments = this.Session.Load<ItemComments>(itemId)
			                            ?? new ItemComments(itemId);

			Comment domainComment = comment.MapTo<Comment>();

			itemComments.AddComment(domainComment, status);

			this.Session.Store(itemComments);
		}

		public IList<CommentDto> GetCommentForSpecificItem(int commentsId, CommentQueryFilter queryFilter)
		{
			if (commentsId < 1)
			{
				throw new ArgumentException("The comments id must be greater than 0", "commentsId");
			}

			ItemComments result = this.Session.Load<ItemComments>(commentsId);

			List<Comment> commentsToMaps = new List<Comment>();

			if (queryFilter.CommentStatus == CommentStatus.Pending)
			{
				commentsToMaps = result.Pending;
			}

			if (queryFilter.CommentStatus == CommentStatus.IsApproved)
			{
				commentsToMaps = result.Approved;
			}

			if (queryFilter.CommentStatus == CommentStatus.IsDeleted)
			{
				commentsToMaps = result.Deleted;
			}

			if (queryFilter.CommentStatus == CommentStatus.IsSpam)
			{
				commentsToMaps = result.Spam;
			}

			return commentsToMaps.MapTo<CommentDto>();
		}

		#endregion
	}
}