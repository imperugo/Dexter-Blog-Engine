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

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Extensions.Logging;
	using Dexter.Services.Events;
	using Dexter.Shared;
	using Dexter.Shared.Exceptions;
	using Dexter.Shared.UserContext;

	public class PageService : IPageService
	{
		#region Fields

		private readonly ILog logger;

		private readonly IPageDataService pageDataService;

		private readonly IUserContext userContext;

		#endregion

		#region Constructors and Destructors

		public PageService(IPageDataService pageDataService, ILog logger, IUserContext userContext)
		{
			this.pageDataService = pageDataService;
			this.logger = logger;
			this.userContext = userContext;
		}

		#endregion

		#region Public Events

		public event EventHandler<CancelEventArgsWithoutParameterWithResult<PageDto>> PageSaved;

		public event EventHandler<CancelEventArgsWithOneParameterWithoutResult<PageDto>> PageSaving;

		public event EventHandler<GenericEventArgs<PageDto>> PageRetrievedById;

		public event EventHandler<GenericEventArgs<PageDto>> PageRetrievedBySlug;

		public event EventHandler<CancelEventArgsWithOneParameter<int, PageDto>> PageRetrievingById;

		public event EventHandler<CancelEventArgsWithOneParameter<string, PageDto>> PageRetrievingBySlug;

		public event EventHandler<GenericEventArgs<IPagedResult<PageDto>>> PagesRetrievedWithFilters;

		public event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PageDto>>> PagesRetrievingWithFilters;

		public event EventHandler<EventArgs> PageDeleted;

		public event EventHandler<CancelEventArgsWithOneParameterWithoutResult<int>> PageDeleting;

		#endregion

		#region Public Methods and Operators

		public void SaveOrUpdate(PageDto item)
		{
			if (item == null)
			{
				throw new DexterPageNotFoundException();
			}

			CancelEventArgsWithOneParameterWithoutResult<PageDto> e = new CancelEventArgsWithOneParameterWithoutResult<PageDto>(item);

			this.PageSaving.Raise(this, e);

			if (e.Cancel)
			{
				return;
			}

			if (item.Id > 0)
			{
				var post = this.pageDataService.GetPageByKey(item.Id);

				if (post == null)
				{
					throw new DexterPostNotFoundException(item.Id);
				}

				if (!this.userContext.IsInRole(Constants.AdministratorRole) && post.Author != this.userContext.Username)
				{
					throw new DexterSecurityException(string.Format("Only the Administrator or the Author can edit the post (item Key '{0}').", post.Id));
				}
			}

			this.pageDataService.SaveOrUpdate(item);

			this.PageSaved.Raise(this, new CancelEventArgsWithoutParameterWithResult<PageDto>(item));
		}

		public string[] GetAllSlugs()
		{
			return this.pageDataService.GetAllSlugs();
		}

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

		public void Delete(int key)
		{
			if (key < 1)
			{
				throw new ArgumentException("The Key must be greater than 0", "key");
			}

			CancelEventArgsWithOneParameterWithoutResult<int> e = new CancelEventArgsWithOneParameterWithoutResult<int>(key);

			this.PageDeleting.Raise(this, e);

			if (e.Cancel)
			{
				return;
			}

			var page = this.pageDataService.GetPageByKey(key);

			if (page == null)
			{
				throw new DexterPostNotFoundException(key);
			}

			if (!this.userContext.IsInRole(Constants.AdministratorRole) && page.Author != this.userContext.Username)
			{
				throw new DexterSecurityException(string.Format("Only the Administrator or the Author can delete the page (item Key '{0}').", page.Id));
			}

			this.pageDataService.Delete(key);

			this.PageDeleted.Raise(this, new EventArgs());
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

		#endregion
	}
}