#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CommentDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using global::AutoMapper;

	using Common.Logging;

	using Dexter.Data.Exceptions;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Indexes.Reading;
	using Dexter.Data.Raven.Session;
	using Dexter.Entities;
	using Dexter.Entities.Filters;

	using global::Raven.Client;

	using global::Raven.Client.Linq;

	public class CommentDataService : ServiceBase, ICommentDataService
	{
		#region Constructors and Destructors

		public CommentDataService(ILog logger, ISessionFactory sessionFactory)
			: base(logger, sessionFactory)
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
			                            ?? new ItemComments();

			Comment domainComment = comment.MapTo<Comment>();

			itemComments.AddComment(domainComment, status);

			this.Session.Store(itemComments);
		}

		public IList<CommentDto> GetCommentForSpecificItem(int commentsId, CommentQueryFilter filters)
		{
			if (commentsId < 1)
			{
				throw new ArgumentException("The comments id must be greater than 0", "commentsId");
			}

			ItemComments result = this.Session.Load<ItemComments>(commentsId);

			List<Comment> commentsToMaps = new List<Comment>();

			if (filters.CommentStatus == CommentStatus.Pending)
			{
				commentsToMaps = result.Pending;
			}

			if (filters.CommentStatus == CommentStatus.IsApproved)
			{
				commentsToMaps = result.Approved;
			}

			if (filters.CommentStatus == CommentStatus.IsSpam)
			{
				commentsToMaps = result.Spam;
			}

			return commentsToMaps.MapTo<CommentDto>();
		}

		public IList<CommentDto> GetRecentApprovedComments(int maxNumberOfComments, ItemQueryFilter filters)
		{
			IRavenQueryable<PostCommentsCreationDateIndex.ReduceResult> query = this.Session.Query<PostCommentsCreationDateIndex.ReduceResult, PostCommentsCreationDateIndex>();

			if (filters != null && filters.Status.HasValue)
			{
				query.Where(x => x.Status == filters.Status);
			}

			if (filters != null && filters.MinPublishAt.HasValue)
			{
				query.Where(x => x.ItemPublishedAt > filters.MaxPublishAt);
			}

			if (filters != null && filters.MaxPublishAt.HasValue)
			{
				query.Where(x => x.ItemPublishedAt < filters.MaxPublishAt);
			}

			List<PostCommentsCreationDateIndex.ReduceResult> data = query.ThenByDescending(x => x.CreatedAt)
			                                                             .AsProjection<PostCommentsCreationDateIndex.ReduceResult>()
			                                                             .Take(maxNumberOfComments)
			                                                             .ToList();

			IList<CommentDto> list = new List<CommentDto>();

			foreach (PostCommentsCreationDateIndex.ReduceResult commentIdentifier in data)
			{
				ItemComments comments = this.Session.Load<ItemComments>(commentIdentifier.PostCommentsId);
				Comment comment = comments.Approved.FirstOrDefault(x => x.Id == commentIdentifier.CommentId);
				Item item = this.Session.Load<Post>(commentIdentifier.ItemId);

				CommentDto c = comment.MapTo<CommentDto>();
				c.ItemInfo = item.MapTo<ItemBaseInfo>();

				list.Add(c);
			}

			return list;
		}

		#endregion
	}
}