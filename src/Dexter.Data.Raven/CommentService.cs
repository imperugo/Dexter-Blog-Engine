#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CommentService.cs
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

namespace Dexter.Data.Raven
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Common.Logging;

	using Dexter.Data.DataTransferObjects;
	using Dexter.Data.DataTransferObjects.Result;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Extensions;

	using global::Raven.Client;

	using global::Raven.Client.Linq;

	public class CommentService : ServiceBase, ICommentService
	{
		#region Constructors and Destructors

		public CommentService(ILog logger, IDocumentSession session)
			: base(logger, session)
		{
		}

		#endregion

		#region Public Methods and Operators

		public PagedResult<CommentDto> GetCommentForSpecificItem(int postId, int pageIndex, int pageSize, CommentQueryFilter queryFilter)
		{
			if (pageIndex < 1)
			{
				throw new ArgumentException("The page index must be greater than 0", "pageIndex");
			}

			if (pageSize < 1)
			{
				throw new ArgumentException("The page size must be greater than 0", "pageSize");
			}

			if (queryFilter == null)
			{
				queryFilter = new CommentQueryFilter
					              {
						              CommentStatus = CommentStatus.IsApproved;
					              };
			}

			RavenQueryStatistics stats;

			IRavenQueryable<Comment> query = this.Session.Query<Comment>()
				.Where(x => x.Status == queryFilter.CommentStatus);

			List<Comment> result = query
				.Statistics(out stats)
				.Take(pageIndex)
				.Skip(pageIndex)
				.ToList();

			List<CommentDto> comments = result.MapTo<CommentDto>();

			if (stats.TotalResults < 1)
			{
				return new EmptyPagedResult<CommentDto>(pageIndex, pageSize);
			}

			return new PagedResult<CommentDto>(pageIndex, pageSize, comments, stats.TotalResults);
		}

		#endregion
	}
}