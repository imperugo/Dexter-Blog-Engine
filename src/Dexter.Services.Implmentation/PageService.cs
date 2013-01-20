#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PageService.cs
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
	using System.Threading.Tasks;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Extensions.Logging;
	using Dexter.Services.Events;

	public class PageService : IPageService
	{
		#region Fields

		private readonly ILog logger;

		private readonly IPageDataService pageDataService;

		#endregion

		#region Constructors and Destructors

		public PageService(IPageDataService pageDataService, ILog logger)
		{
			this.pageDataService = pageDataService;
			this.logger = logger;
		}

		#endregion

		#region Public Events

		public event EventHandler<GenericEventArgs<PageDto>> PageRetrievedById;

		public event EventHandler<GenericEventArgs<PageDto>> PageRetrievedBySlug;

		public event EventHandler<CancelEventArgsWithOneParameter<int, PageDto>> PageRetrievingById;

		public event EventHandler<CancelEventArgsWithOneParameter<string, PageDto>> PageRetrievingBySlug;

		public event EventHandler<GenericEventArgs<IPagedResult<PageDto>>> PagesRetrievedWithFilters;

		public event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PageDto>>> PagesRetrievingWithFilters;

		#endregion

		#region Public Methods and Operators

		public PageDto GetPageByKey(int key)
		{
			if (key < 1)
			{
				throw new ArgumentException("The Key must be greater than 0", "key");
			}

			CancelEventArgsWithOneParameter<int, PageDto> e = new CancelEventArgsWithOneParameter<int, PageDto>(key, null);

			this.PageRetrievingById.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'GetPageByKey' is overridden by the event 'PageRetrievingById'.");
				return e.Result;
			}

			PageDto result = this.pageDataService.GetPageByKey(key);

			this.PageRetrievedById.Raise(this, new GenericEventArgs<PageDto>(result));

			return result;
		}

		public Task<PageDto> GetPageByKeyAsync(int key)
		{
			return Task.Run(() => this.GetPageByKey(key));
		}

		public PageDto GetPageBySlug(string slug)
		{
			if (slug == null)
			{
				throw new ArgumentNullException("slug");
			}

			if (slug == string.Empty)
			{
				throw new ArgumentException("The string must have a value.", "slug");
			}

			CancelEventArgsWithOneParameter<string, PageDto> e = new CancelEventArgsWithOneParameter<string, PageDto>(slug, null);

			this.PageRetrievingBySlug.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'PageRetrievingBySlug' is overridden by the event 'PageRetrievingBySlug'.");
				return e.Result;
			}

			PageDto result = this.pageDataService.GetPageBySlug(slug);

			this.PageRetrievedBySlug.Raise(this, new GenericEventArgs<PageDto>(result));

			return result;
		}

		public Task<PageDto> GetPageBySlugAsync(string slug)
		{
			return Task.Run(() => this.GetPageBySlug(slug));
		}

		public IPagedResult<PageDto> GetPages(int pageIndex, int pageSize, ItemQueryFilter filters)
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

			CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PageDto>> e = new CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PageDto>>(new Tuple<int, int, ItemQueryFilter>(pageIndex, pageSize, filters), null);

			this.PagesRetrievingWithFilters.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'GetPages' is overridden by the event 'PagesRetrievingWithFilters'.");
				return e.Result;
			}

			IPagedResult<PageDto> result = this.pageDataService.GetPages(pageIndex, pageSize, filters);

			this.PagesRetrievedWithFilters.Raise(this, new GenericEventArgs<IPagedResult<PageDto>>(result));

			return result;
		}

		public Task<IPagedResult<PageDto>> GetPagesAsync(int pageIndex, int pageSize, ItemQueryFilter filter)
		{
			return Task.Run(() => this.GetPagesAsync(pageIndex, pageSize, filter));
		}

		#endregion
	}
}