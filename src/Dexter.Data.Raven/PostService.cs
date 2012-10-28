#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostService.cs
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
	using Dexter.Data.Exceptions;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Extensions;

	using global::Raven.Client;

	using global::Raven.Client.Linq;

	public class PostService : ServiceBase, IPostService
	{
		#region Constructors and Destructors

		public PostService(ILog logger, IDocumentSession session)
			: base(logger, session)
		{
		}

		#endregion

		#region Public Methods and Operators

		public void Delete(int id)
		{
			Post post = this.Session.Load<Post>(id);

			if (post == null)
			{
				throw new ItemNotFoundException(id);
			}

			this.Session.Delete(post);
		}

		public PostDto GetPostDtoById(int id)
		{
			if (id < 1)
			{
				throw new ArgumentException("The Id must be greater than 0", "id");
			}

			Post post = this.Session.Query<Post>()
				.Where(x => x.Id == id)
				.Include(x => x.CommentsId)
				.Include(x => x.CategoriesId)
				.First();

			if (post == null)
			{
				throw new ItemNotFoundException(id);
			}

			PostDto result = post.MapTo<PostDto>();

			return result;
		}

		public PostDto GetPostDtoByKey(string slug)
		{
			if (slug == null)
			{
				throw new ArgumentNullException("slug");
			}

			if (slug == string.Empty)
			{
				throw new ArgumentException("The string must have a value.", "slug");
			}

			Post post = this.Session.Query<Post>().Where(x => x.Slug == slug)
				.Include(x => x.CommentsId)
				.Include(x => x.CategoriesId).First();

			if (post == null)
			{
				throw new ItemNotFoundException(id);
			}

			PostDto result = post.MapTo<PostDto>();

			return result;
		}

		public IPagedResult<PostDto> GetPosts(int pageIndex, int pageSize, PostQueryFilter filter)
		{
			if (pageIndex < 1)
			{
				throw new ArgumentException("The page index must be greater than 0", "pageIndex");
			}

			if (pageSize < 1)
			{
				throw new ArgumentException("The page size must be greater than 0", "pageSize");
			}

			RavenQueryStatistics stats;

			IRavenQueryable<Post> query = this.Session.Query<Post>();

			if (filter != null && filter.Status.HasValue)
			{
				query.Where(x => x.Status == filter.Status);
			}

			if (filter != null && filter.MinPublishAt.HasValue)
			{
				query.Where(x => x.PublishAt > filter.MaxPublishAt);
			}

			if (filter != null && filter.MaxPublishAt.HasValue)
			{
				query.Where(x => x.PublishAt < filter.MaxPublishAt);
			}

			List<Post> result = query
				.Include(x => x.CommentsId)
				.Include(x => x.CategoriesId)
				.Statistics(out stats)
				.Take(pageIndex)
				.Skip(pageIndex)
				.ToList();

			List<PostDto> posts = result.MapTo<PostDto>();

			if (stats.TotalResults < 1)
			{
				return new EmptyPagedResult<PostDto>(pageIndex, pageSize);
			}

			return new PagedResult<PostDto>(pageIndex, pageSize, posts, stats.TotalResults);
		}

		#endregion
	}
}