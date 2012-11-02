#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/11/02
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

	using Common.Logging;

	using Dexter.Data.Exceptions;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Extensions;
	using Dexter.Data.Raven.Helpers;
	using Dexter.Data.Raven.Indexes;
	using Dexter.Data.Raven.Session;
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;

	using global::Raven.Client;

	using global::Raven.Client.Linq;

	public class PostDataService : ServiceBase, IPostDataService
	{
		#region Constructors and Destructors

		public PostDataService(ILog logger, ISessionFactory sessionFactory)
			: base(logger, sessionFactory)
		{
		}

		#endregion

		#region Public Methods and Operators

		public void Delete(int id)
		{
			Post post = this.Session.Load<Post>(id);

			if (post == null)
			{
				throw new ItemNotFoundException("id");
			}

			this.Session.Delete(post);
		}

		public PostDto GetPostByKey(int id)
		{
			if (id < 1)
			{
				throw new ArgumentException("The Id must be greater than 0", "id");
			}

			Post post = this.Session.Query<Post>()
				.Where(x => x.Id == id)
				.Include(x => x.CommentsId)
				.Include(x => x.CategoriesId).First();

			if (post == null)
			{
				throw new ItemNotFoundException("id");
			}

			PostDto result = post.MapTo<PostDto>();

			return result;
		}

		public PostDto GetPostBySlug(string slug)
		{
			Post post = this.GetPostBySlugInternal(slug);

			if (post == null)
			{
				throw new ItemNotFoundException("slug");
			}

			PostDto result = post.MapTo<PostDto>();

			return result;
		}

		public IList<MonthDto> GetMonthsForPublishedPosts()
		{
			var now = DateTime.Now;

			var dates = this.Session.Query<MonthDto, PostsByMonthPublishedCount>()
				.OrderByDescending(x => x.Year)
				.ThenByDescending(x => x.Month)
				.Where(x => (x.Year < now.Year || x.Year == now.Year) && x.Month <= now.Month)
				.ToList();

			return dates;
		}

		public IPagedResult<PostDto> GetPosts(int pageIndex, int pageSize, ItemQueryFilter filters)
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

			List<Post> result = this.Session.Query<Post>()
				.ApplyFilterItem(filters)
				.OrderByDescending(post => post.PublishAt)
				.Statistics(out stats)
				.Paging(pageIndex, pageSize)
				.ToList();

			List<PostDto> posts = result.MapTo<PostDto>();

			if (stats.TotalResults < 1)
			{
				return new EmptyPagedResult<PostDto>(pageIndex, pageSize);
			}

			return new PagedResult<PostDto>(pageIndex, pageSize, posts, stats.TotalResults);
		}

		public IPagedResult<PostDto> GetPostsByDate(int pageIndex, int pageSize, int year, int? month, int? day, ItemQueryFilter filters)
		{
			if (pageIndex < 1)
			{
				throw new ArgumentException("The page index must be greater than 0", "pageIndex");
			}

			if (pageSize < 1)
			{
				throw new ArgumentException("The page size must be greater than 0", "pageSize");
			}

			if (year < 1700)
			{
				throw new ArgumentException("The year value must be greater than 1700. Internet did not exist in 1700!", "year");
			}

			if (month.HasValue && (month.Value < 1 || month.Value > 12))
			{
				throw new ArgumentException("The month value must be greater than 0 and lesser than 12", "month");
			}

			if (day.HasValue && (day.Value < 1 || day.Value > 31))
			{
				throw new ArgumentException("The day value must be greater than 0 and lesser than 31", "month");
			}

			RavenQueryStatistics stats;

			IRavenQueryable<Post> query = this.Session.Query<Post>()
				.Where(post => post.PublishAt.Year == year)
				.ApplyFilterItem(filters);

			if (month.HasValue)
			{
				query.Where(post => post.PublishAt.Month == month.Value);
			}

			if (day.HasValue)
			{
				query.Where(post => post.PublishAt.Day == day.Value);
			}

			List<Post> result = query.Include(x => x.CommentsId)
				.Statistics(out stats)
				.OrderByDescending(post => post.PublishAt)
				.Paging(pageIndex, pageSize)
				.ToList();

			List<PostDto> posts = result.MapTo<PostDto>();

			if (stats.TotalResults < 1)
			{
				return new EmptyPagedResult<PostDto>(pageIndex, pageSize);
			}

			return new PagedResult<PostDto>(pageIndex, pageSize, posts, stats.TotalResults);
		}

		public IPagedResult<PostDto> GetPostsByTag(int pageIndex, int pageSize, string tag, ItemQueryFilter filters)
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

			List<Post> result = this.Session.Query<Post>()
				.ApplyFilterItem(filters)
				.Statistics(out stats)
				.Where(post => post.Tags.Any(postTag => postTag == tag))
				.OrderByDescending(post => post.PublishAt)
				.Paging(pageIndex, pageSize)
				.ToList();

			List<PostDto> posts = result.MapTo<PostDto>();

			if (stats.TotalResults < 1)
			{
				return new EmptyPagedResult<PostDto>(pageIndex, pageSize);
			}

			return new PagedResult<PostDto>(pageIndex, pageSize, posts, stats.TotalResults);
		}

		public void SaveOrUpdate(PostDto item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", "The post item must be contains a valid instance");
			}

			Post itemToSave = item.MapTo<Post>();

			SlugHelper.GenerateSlug(itemToSave, this.GetPostBySlugInternal);

			this.Session.Store(itemToSave);
		}

		#endregion

		#region Methods

		private Post GetPostBySlugInternal(string slug)
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

			return post;
		}

		#endregion
	}
}