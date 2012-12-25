#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostDataService.cs
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
	using Dexter.Data.Raven.Extensions;
	using Dexter.Data.Raven.Helpers;
	using Dexter.Data.Raven.Indexes.Reading;
	using Dexter.Data.Raven.Indexes.Updating;
	using Dexter.Data.Raven.Session;
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;

	using global::Raven.Client;

	using global::Raven.Client.Linq;

	public class PostDataService : ServiceBase, IPostDataService
	{
		#region Fields

		private readonly IDocumentStore store;

		#endregion

		#region Constructors and Destructors

		public PostDataService(ILog logger, ISessionFactory sessionFactory, IDocumentStore store)
			: base(logger, sessionFactory)
		{
			this.store = store;
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

		public IList<MonthDto> GetMonthsForPublishedPosts()
		{
			DateTime now = DateTime.Now;

			List<MonthDto> dates = this.Session.Query<MonthDto, MonthOfPublishedPostsWithCountIndex>()
			                           .OrderByDescending(x => x.Year)
			                           .ThenByDescending(x => x.Month)
			                           .Where(x => (x.Year < now.Year || x.Year == now.Year) && x.Month <= now.Month)
			                           .ToList();

			return dates;
		}

		public PostDto GetPostByKey(int id)
		{
			if (id < 1)
			{
				throw new ArgumentException("The Id must be greater than 0", "id");
			}

			Post post = this.Session
			                .Include<Post>(x => x.CommentsId)
			                .Include<Post>(x => x.TrackbacksId)
			                .Load(id);

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
									.Statistics(out stats)
			                        .ApplyFilterItem(filters)
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

			var query = this.Session.Query<Post>()
												.Statistics(out stats)
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

			List<Post> result = query
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
									.Statistics(out stats)
			                        .ApplyFilterItem(filters)
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

		public IList<TagDto> GetTopTagsForPublishedPosts(int numberOfTags)
		{
			if (numberOfTags < 1)
			{
				throw new ArgumentException("The number of tags to retrieve must be greater than 0", "numberOfTags");
			}

			return this.Session.Query<TagDto, TagsForPublishedPostsWithCountIndex>()
			           .OrderBy(x => x.Count)
			           .Take(numberOfTags)
			           .ToList();
		}

		public void SaveOrUpdate(PostDto item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", "The post must be contains a valid instance");
			}

			Post post = this.Session.Load<Post>(item.Id)
			            ?? new Post
				               {
					               CreatedAt = DateTimeOffset.Now
				               };

			bool mustUpdateDenormalizedObject = false;

			if (!item.IsTransient)
			{
				mustUpdateDenormalizedObject = post.MustUpdateDenormalizedObject(item);
			}

			item.MapPropertiesToInstance(post);

			this.Session.Store(post);

			if (item.IsTransient)
			{
				post.Slug = SlugHelper.GenerateSlug(post, this.GetPostBySlugInternal);

				ItemComments comments = new ItemComments
					                        {
						                        Item = new ItemReference
							                               {
								                               Id = post.Id, 
								                               Status = post.Status, 
								                               ItemPublishedAt = post.PublishAt
							                               }
					                        };

				this.Session.Store(comments);
				post.CommentsId = comments.Id;
			}

			if (mustUpdateDenormalizedObject)
			{
				UpdateDenormalizedItemIndex.UpdateIndexes(this.store, this.Session, post);
			}

			item.Id = post.Id;
		}

		public void SaveTrackback(TrackBackDto trackBack, int itemId)
		{
			if (trackBack == null)
			{
				throw new ArgumentNullException("trackBack", "The trackBack must be contains a valid instance");
			}

			if (itemId < 1)
			{
				throw new ArgumentException("The Id must be greater than 0", "itemId");
			}

			Post post = this.Session
			                .Include<Post>(x => x.TrackbacksId)
			                .Load<Post>(itemId);

			if (post == null)
			{
				throw new ItemNotFoundException("itemId");
			}

			ItemTrackbacks trackbacks = this.Session.Load<ItemTrackbacks>(post.TrackbacksId)
			                            ?? new ItemTrackbacks
				                               {
					                               Item = new ItemReference
						                                      {
							                                      Id = post.Id, 
							                                      Status = post.Status, 
							                                      ItemPublishedAt = post.PublishAt
						                                      }
				                               };

			trackbacks.AddTrackback(trackBack.MapTo<Trackback>(), trackBack.Status);

			this.Session.Store(trackbacks);
			post.TrackbacksId = trackbacks.Id;

			this.Session.Store(post);
		}

		public IPagedResult<PostDto> Search(string term, int pageIndex, int pageSize, ItemQueryFilter filters)
		{
			RavenQueryStatistics stats;

			var result = this.Session.Query<PostFullTextIndex.ReduceResult, PostFullTextIndex>()
			                .Search(x => x.SearchQuery, term)
			                .OrderByDescending(post => post.PublishDate)
			                .Statistics(out stats)
			                .As<Post>()
							.ApplyFilterItem(filters)
							.Paging(pageIndex, pageSize)
							.ToList();

			List<PostDto> posts = result.MapTo<PostDto>();

			if (stats.TotalResults < 1)
			{
				return new EmptyPagedResult<PostDto>(pageIndex, pageSize);
			}

			return new PagedResult<PostDto>(pageIndex, pageSize, posts, stats.TotalResults);
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

			Post post = this.Session.Query<Post>()
			                .Where(x => x.Slug == slug)
			                .Include(x => x.CommentsId)
			                .Include(x => x.TrackbacksId)
			                .FirstOrDefault();

			return post;
		}

		#endregion
	}
}