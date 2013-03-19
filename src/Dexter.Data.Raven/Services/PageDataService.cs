#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PageDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/02
// Last edit:	2013/03/18
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Services
{
	using System;
	using System.Linq;
	using System.Threading;

	using global::AutoMapper;

	using Common.Logging;

	using Dexter.Data.Exceptions;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Extensions;
	using Dexter.Data.Raven.Helpers;
	using Dexter.Data.Raven.Indexes.Updating;
	using Dexter.Data.Raven.Session;
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;

	using global::Raven.Client;

	using global::Raven.Client.Linq;

	public class PageDataService : ServiceBase, IPageDataService
	{
		#region Fields

		private readonly IDocumentStore store;

		#endregion

		#region Constructors and Destructors

		public PageDataService(ILog logger, ISessionFactory sessionFactory, IDocumentStore store)
			: base(logger, sessionFactory)
		{
			this.store = store;
		}

		#endregion

		#region Public Methods and Operators

		public void Delete(int id)
		{
			Page page = this.Session
			                .Include<Page>(x => x.CommentsId)
			                .Include<Page>(x => x.TrackbacksId)
			                .Load<Page>(id);

			ItemComments comments = this.Session.Load<ItemComments>(page.CommentsId);
			ItemComments trackbacks = this.Session.Load<ItemComments>(page.TrackbacksId);

			if (page == null)
			{
				throw new ItemNotFoundException("id");
			}

			this.Session.Delete(page);
			this.Session.Delete(comments);
			this.Session.Delete(trackbacks);
		}

		public PageDto GetPageByKey(int id)
		{
			if (id < 1)
			{
				throw new ArgumentException("The Id must be greater than 0", "id");
			}

			Page page = this.Session
			                .Include<Page>(x => x.CommentsId)
			                .Load(id);

			if (page == null)
			{
				throw new ItemNotFoundException("id");
			}

			PageDto result = page.MapTo<PageDto>();

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

			Page page = this.Session.Query<Page>()
			                .Where(x => x.Slug == slug)
			                .Include(x => x.CommentsId)
			                .FirstOrDefault();

			if (page == null)
			{
				throw new ItemNotFoundException("slug");
			}

			PageDto result = page.MapTo<PageDto>();

			return result;
		}

		public IPagedResult<PageDto> GetPages(int pageIndex, int pageSize, ItemQueryFilter filter)
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

			return this.Session.Query<Page>()
			           .Statistics(out stats)
			           .ApplyFilterItem(filter)
			           .OrderByDescending(post => post.PublishAt)
			           .ToPagedResult<Page, PageDto>(pageIndex, pageSize, stats);
		}

		public void SaveOrUpdate(PageDto item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", "The post must be contains a valid instance");
			}

			Page page = this.Session.Load<Page>(item.Id)
			            ?? new Page
				               {
					               CreatedAt = DateTimeOffset.Now
				               };

			if (string.IsNullOrEmpty(item.Author))
			{
				page.Author = Thread.CurrentPrincipal.Identity.Name;
			}

			bool mustUpdateDenormalizedObject = false;

			if (!item.IsTransient)
			{
				mustUpdateDenormalizedObject = page.MustUpdateDenormalizedObject(item);
			}

			item.MapPropertiesToInstance(page);

			if (string.IsNullOrEmpty(page.Excerpt))
			{
				page.Excerpt = AbstractHelper.GenerateAbstract(page.Content);
			}

			if (string.IsNullOrEmpty(page.Slug))
			{
				page.Slug = SlugHelper.GenerateSlug(page, this.GetPostBySlugInternal);
			}

			this.Session.Store(page);

			if (page.IsTransient)
			{
				ItemComments comments = new ItemComments
					                        {
						                        Item = new ItemReference
							                               {
								                               Id = page.Id, 
								                               Status = page.Status, 
								                               ItemPublishedAt = page.PublishAt
							                               }
					                        };

				this.Session.Store(comments);
				page.CommentsId = comments.Id;

				ItemTrackbacks trackbacks = new ItemTrackbacks
					                            {
						                            Item = new ItemReference
							                                   {
								                                   Id = page.Id, 
								                                   Status = page.Status, 
								                                   ItemPublishedAt = page.PublishAt
							                                   }
					                            };

				this.Session.Store(trackbacks);
				page.TrackbacksId = trackbacks.Id;
			}

			if (mustUpdateDenormalizedObject)
			{
				UpdateDenormalizedItemIndex.UpdateIndexes(this.store, this.Session, page);
			}

			item.Id = RavenIdHelper.Resolve(page.Id);
		}

		#endregion

		#region Methods

		private Page GetPostBySlugInternal(string slug)
		{
			if (slug == null)
			{
				throw new ArgumentNullException("slug");
			}

			if (slug == string.Empty)
			{
				throw new ArgumentException("The string must have a value.", "slug");
			}

			Page page = this.Session.Query<Page>()
			                .Where(x => x.Slug == slug)
			                .Include(x => x.CommentsId)
			                .Include(x => x.TrackbacksId)
			                .FirstOrDefault();

			return page;
		}

		#endregion
	}
}