#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services.Implmentation
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Data.Exceptions;
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Extensions.Logging;
	using Dexter.Services.Events;

	public class PostService : IPostService
	{
		#region Fields

		private readonly ILog logger;

		private readonly IPostDataService postDataService;

		#endregion

		#region Constructors and Destructors

		public PostService(IPostDataService postDataService, ILog logger)
		{
			this.postDataService = postDataService;
			this.logger = logger;
		}

		#endregion

		#region Public Events

		public event EventHandler<GenericEventArgs<IList<MonthDto>>> MonthsRetrievedForPublishedPosts;

		public event EventHandler<CancelEventArgsWithoutParameters<IList<MonthDto>>> MonthsRetrievingForPublishedPosts;

		public event EventHandler<EventArgs> PostDeleted;

		public event EventHandler<CancelEventArgsWithOneParameterWithoutResult<int>> PostDeleting;

		public event EventHandler<GenericEventArgs<PostDto>> PostRetrievedById;

		public event EventHandler<GenericEventArgs<PostDto>> PostRetrievedBySlug;

		public event EventHandler<CancelEventArgsWithOneParameter<int, PostDto>> PostRetrievingById;

		public event EventHandler<CancelEventArgsWithOneParameter<string, PostDto>> PostRetrievingBySlug;

		public event EventHandler<CancelEventArgsWithoutParameterWithResult<PostDto>> PostSaved;

		public event EventHandler<CancelEventArgsWithOneParameterWithoutResult<PostDto>> PostSaving;

		public event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedByDates;

		public event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedByTag;

		public event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedWithFilters;

		public event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, int, int?, int?, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingByDates;

		public event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, string, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingBytag;

		public event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingWithFilters;

		public event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsSearchedWithFilters;

		public event EventHandler<CancelEventArgsWithOneParameter<Tuple<string, int, int, ItemQueryFilter>, IPagedResult<PostDto>>> PostsSearchingWithFilters;

		public event EventHandler<GenericEventArgs<IList<TagDto>>> TagsRetrievedForPublishedPosts;

		public event EventHandler<CancelEventArgsWithOneParameter<int, IList<TagDto>>> TagsRetrievingForPublishedPosts;

		#endregion

		#region Public Methods and Operators

		public void Delete(int key)
		{
			if (key < 1)
			{
				throw new ArgumentException("The Key must be greater than 0", "key");
			}

			CancelEventArgsWithOneParameterWithoutResult<int> e = new CancelEventArgsWithOneParameterWithoutResult<int>(key);

			this.PostDeleting.Raise(this, e);

			if (e.Cancel)
			{
				return;
			}

			this.postDataService.Delete(key);

			this.PostDeleted.Raise(this, new EventArgs());
		}

		public IList<MonthDto> GetMonthsForPublishedPosts()
		{
			CancelEventArgsWithoutParameters<IList<MonthDto>> e = new CancelEventArgsWithoutParameters<IList<MonthDto>>(null);

			this.MonthsRetrievingForPublishedPosts.Raise(this, e);

			if (e.Cancel)
			{
				return e.Result;
			}

			IList<MonthDto> data = this.postDataService.GetMonthsForPublishedPosts();

			this.MonthsRetrievedForPublishedPosts.Raise(this, new GenericEventArgs<IList<MonthDto>>(data));

			return data;
		}

		public PostDto GetPostByKey(int key)
		{
			if (key < 1)
			{
				throw new ArgumentException("The Key must be greater than 0", "key");
			}

			CancelEventArgsWithOneParameter<int, PostDto> e = new CancelEventArgsWithOneParameter<int, PostDto>(key, null);

			this.PostRetrievingById.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'GetPostByKey' is overridden by the event 'PostRetrievingById'.");
				return e.Result;
			}

			PostDto result = this.postDataService.GetPostByKey(key);

			this.PostRetrievedById.Raise(this, new GenericEventArgs<PostDto>(result));

			return result;
		}

		public PostDto GetPostBySlug(string slug)
		{
			if (slug == null)
			{
				throw new ArgumentNullException("slug");
			}

			if (slug == string.Empty)
			{
				throw new ArgumentException("The string must have a value.", "slug");
			}

			CancelEventArgsWithOneParameter<string, PostDto> e = new CancelEventArgsWithOneParameter<string, PostDto>(slug, null);

			this.PostRetrievingBySlug.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'PostRetrievingBySlug' is overridden by the event 'PostRetrievingBySlug'.");
				return e.Result;
			}

			PostDto result = this.postDataService.GetPostBySlug(slug);

			this.PostRetrievedBySlug.Raise(this, new GenericEventArgs<PostDto>(result));

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

			if (filters == null)
			{
				filters = new ItemQueryFilter();
				filters.MaxPublishAt = DateTime.Now;
				filters.Status = ItemStatus.Published;
			}

			CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PostDto>> e = new CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PostDto>>(new Tuple<int, int, ItemQueryFilter>(pageIndex, pageSize, filters), null);

			this.PostsRetrievingWithFilters.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'GetPosts' is overridden by the event 'PostsRetrievingWithFilters'.");
				return e.Result;
			}

			IPagedResult<PostDto> result = this.postDataService.GetPosts(pageIndex, pageSize, filters);

			this.PostsRetrievedWithFilters.Raise(this, new GenericEventArgs<IPagedResult<PostDto>>(result));

			return result;
		}

		public IPagedResult<PostDto> GetPostsByDate(int pageIndex, int pageSize, int year, int? month, int? day, ItemQueryFilter filters = null)
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

			if (filters == null)
			{
				filters = new ItemQueryFilter();
				filters.MaxPublishAt = DateTimeOffset.Now.AsMinutes();
				filters.Status = ItemStatus.Published;
			}

			Tuple<int, int, int, int?, int?, ItemQueryFilter> p = new Tuple<int, int, int, int?, int?, ItemQueryFilter>(pageIndex, pageSize, year, month, day, filters);

			CancelEventArgsWithOneParameter<Tuple<int, int, int, int?, int?, ItemQueryFilter>, IPagedResult<PostDto>> e = new CancelEventArgsWithOneParameter<Tuple<int, int, int, int?, int?, ItemQueryFilter>, IPagedResult<PostDto>>(p, null);

			this.PostsRetrievingByDates.Raise(this, e);

			if (e.Cancel)
			{
				return e.Result;
			}

			IPagedResult<PostDto> result = this.postDataService.GetPostsByDate(pageIndex, pageSize, year, month, day, filters);

			this.PostsRetrievedByDates.Raise(this, new GenericEventArgs<IPagedResult<PostDto>>(result));

			return result;
		}

		public IPagedResult<PostDto> GetPostsByTag(int pageIndex, int pageSize, string tag, ItemQueryFilter filters = null)
		{
			if (pageIndex < 1)
			{
				throw new ArgumentException("The page index must be greater than 0", "pageIndex");
			}

			if (pageSize < 1)
			{
				throw new ArgumentException("The page size must be greater than 0", "pageSize");
			}

			if (tag == null)
			{
				throw new ArgumentNullException("tag", "The string tag must contains a value");
			}

			if (tag == string.Empty)
			{
				throw new ArgumentException("The string tag must not be empty", "tag");
			}

			if (filters == null)
			{
				filters = new ItemQueryFilter();
				filters.MaxPublishAt = DateTimeOffset.Now.AsMinutes();
				filters.Status = ItemStatus.Published;
			}

			CancelEventArgsWithOneParameter<Tuple<int, int, string, ItemQueryFilter>, IPagedResult<PostDto>> e = new CancelEventArgsWithOneParameter<Tuple<int, int, string, ItemQueryFilter>, IPagedResult<PostDto>>(new Tuple<int, int, string, ItemQueryFilter>(pageIndex, pageSize, tag, filters), null);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'GetPostsByTag' is overridden by the event 'PostsRetrievingBytag'.");
				return e.Result;
			}

			IPagedResult<PostDto> result = this.postDataService.GetPostsByTag(pageIndex, pageSize, tag, filters);

			this.PostsRetrievedByTag.Raise(this, new GenericEventArgs<IPagedResult<PostDto>>(result));

			return result;
		}

		public IList<TagDto> GetTopTagsForPublishedPosts(int maxNumberOfTags)
		{
			if (maxNumberOfTags < 1)
			{
				throw new ArgumentException("The number of tags to retrieve must be greater than 0", "maxNumberOfTags");
			}

			CancelEventArgsWithOneParameter<int, IList<TagDto>> e = new CancelEventArgsWithOneParameter<int, IList<TagDto>>(maxNumberOfTags, null);

			this.TagsRetrievingForPublishedPosts.Raise(this, e);

			if (e.Cancel)
			{
				return e.Result;
			}

			IList<TagDto> data = this.postDataService.GetTopTagsForPublishedPosts(maxNumberOfTags);

			this.TagsRetrievedForPublishedPosts.Raise(this, new GenericEventArgs<IList<TagDto>>(data));

			return data;
		}

		public void SaveOrUpdate(PostDto item)
		{
			if (item == null)
			{
				throw new ItemNotFoundException("item");
			}

			CancelEventArgsWithOneParameterWithoutResult<PostDto> e = new CancelEventArgsWithOneParameterWithoutResult<PostDto>(item);

			this.PostSaving.Raise(this, e);

			if (e.Cancel)
			{
				return;
			}

			this.postDataService.SaveOrUpdate(item);

			this.PostSaved.Raise(this, new CancelEventArgsWithoutParameterWithResult<PostDto>(item));
		}

		public IPagedResult<PostDto> Search(string term, int pageIndex, int pageSize, ItemQueryFilter filters)
		{
			if (pageIndex < 1)
			{
				throw new ArgumentException("The page index must be greater than 0", "pageIndex");
			}

			if (pageSize < 1)
			{
				throw new ArgumentException("The page size must be greater than 0", "pageSize");
			}

			if (filters == null)
			{
				filters = new ItemQueryFilter();
				filters.MaxPublishAt = DateTime.Now;
				filters.Status = ItemStatus.Published;
			}

			CancelEventArgsWithOneParameter<Tuple<string, int, int, ItemQueryFilter>, IPagedResult<PostDto>> e = new CancelEventArgsWithOneParameter<Tuple<string, int, int, ItemQueryFilter>, IPagedResult<PostDto>>(new Tuple<string, int, int, ItemQueryFilter>(term, pageIndex, pageSize, filters), null);

			this.PostsSearchingWithFilters.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'Search' is overridden by the event 'PostsSearchingWithFilters'.");
				return e.Result;
			}

			IPagedResult<PostDto> result = this.postDataService.Search(term, pageIndex, pageSize, filters);

			this.PostsSearchedWithFilters.Raise(this, new GenericEventArgs<IPagedResult<PostDto>>(result));

			return result;
		}

		#endregion
	}
}