#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			QueryExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2013/01/06
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Extensions
{
	using System.Collections.Generic;
	using System.Linq;

	using global::AutoMapper;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;

	using global::Raven.Client;

	public static class QueryExtensions
	{
		#region Public Methods and Operators

		public static IQueryable<T> ApplyFilterItem<T>(this IQueryable<T> query, ItemQueryFilter filters) where T : Item
		{
			if (filters != null && filters.Status.HasValue)
			{
				query.Where(x => x.Status == filters.Status);
			}

			if (filters != null && filters.MinPublishAt.HasValue)
			{
				query.Where(x => x.PublishAt > filters.MaxPublishAt);
			}

			if (filters != null && filters.MaxPublishAt.HasValue)
			{
				query.Where(x => x.PublishAt < filters.MaxPublishAt);
			}

			return query;
		}

		public static IQueryable<T> Paging<T>(this IQueryable<T> query, int currentPage, int pageSize)
		{
			return query
				.Skip((currentPage - 1) * pageSize)
				.Take(pageSize);
		}

		public static IPagedResult<TK> ToPagedResult<T,TK>(this IQueryable<T> query, int currentPage, int pageSize, RavenQueryStatistics stats)
		{
			List<T> data = query.Paging(currentPage, pageSize).ToList();

			if (stats.TotalResults < 1)
			{
				return new EmptyPagedResult<TK>(currentPage, pageSize);
			}

			List<TK> posts = data.MapTo<TK>();

			return new PagedResult<TK>(currentPage, pageSize, posts, stats.TotalResults);
		}

		#endregion
	}
}