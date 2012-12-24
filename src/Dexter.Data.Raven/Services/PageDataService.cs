#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PageDataService.cs
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
	using Dexter.Data.Raven.Session;
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;

	using global::Raven.Client;

	using global::Raven.Client.Linq;

	public class PageDataService : ServiceBase, IPageDataService
	{
		#region Constructors and Destructors

		public PageDataService(ILog logger, ISessionFactory sessionFactory)
			: base(logger, sessionFactory)
		{
		}

		#endregion

		#region Public Methods and Operators

		public void Delete(int id)
		{
			Page page = this.Session.Load<Page>(id);

			if (page == null)
			{
				throw new ItemNotFoundException("id");
			}

			this.Session.Delete(page);
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

			List<Page> result = this.Session.Query<Page>()
			                        .ApplyFilterItem(filter)
			                        .OrderByDescending(post => post.PublishAt)
			                        .Statistics(out stats)
			                        .Paging(pageIndex, pageSize)
			                        .ToList();

			List<PageDto> posts = result.MapTo<PageDto>();

			if (stats.TotalResults < 1)
			{
				return new EmptyPagedResult<PageDto>(pageIndex, pageSize);
			}

			return new PagedResult<PageDto>(pageIndex, pageSize, posts, stats.TotalResults);
		}

		#endregion
	}
}