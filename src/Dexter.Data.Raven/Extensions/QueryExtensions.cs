#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			QueryExtensions.cs
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

namespace Dexter.Data.Raven.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Dexter.Entities;

	using global::AutoMapper;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;

	using global::Raven.Client;
	using global::Raven.Client.Linq;

	public static class QueryExtensions
	{
		#region Public Methods and Operators

		public static IQueryable<T> ApplyFilterItem<T>(this IQueryable<T> query, ItemQueryFilter filters) where T : Item
		{
			if (filters != null && filters.Status.HasValue)
			{
				query = query.Where(x => x.Status == filters.Status);
			}

			if (filters != null && filters.MinPublishAt.HasValue)
			{
				query = query.Where(x => x.PublishAt >= filters.MaxPublishAt);
			}

			if (filters != null && filters.MaxPublishAt.HasValue)
			{
				query = query.Where(x => x.PublishAt <= filters.MaxPublishAt);
			}

			return query;
		}

		public static IQueryable<T> Paging<T>(this IQueryable<T> query, int currentPage, int pageSize)
		{
			return query
				.Skip((currentPage - 1) * pageSize)
				.Take(pageSize);
		}

		public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> query, ItemQueryFilter filters) where T : Item
		{
			if (filters == null)
			{
				query = query.OrderByDescending(x => x.CreatedAt);
				return query;
			}

			if (filters.OrderFilter.RandomOrder)
			{
				var q = query as IRavenQueryable<T>;

				if (q != null)
				{
					query = q.Customize(x => x.RandomOrdering());
				}

				return query;
			}

			if (filters.OrderFilter.Ascending)
			{
				switch (filters.OrderFilter.SortBy)
				{
						case SortBy.CreatedAt:
						{
							query = query.OrderBy(x => x.CreatedAt);
							break;
						}
						case SortBy.Id:
						{
							query = query.OrderBy(x => x.Id);
							break;
						}

						case SortBy.PublishAt:
						{
							query = query.OrderBy(x => x.PublishAt);
							break;
						}

						case SortBy.Title:
						{
							query = query.OrderBy(x => x.Title);
							break;
						}

						case SortBy.TotalComments:
						{
							query = query.OrderBy(x => x.TotalComments);
							break;
						}
						case SortBy.TotalTrackback:
						{
							query = query.OrderBy(x => x.TotalTrackback);
							break;
						}
						default:
						{
							query = query.OrderBy(x => x.PublishAt);
							break;
						}
				}
				
				return query;
			}

			switch (filters.OrderFilter.SortBy)
			{
				case SortBy.CreatedAt:
					{
						query = query.OrderByDescending(x => x.CreatedAt);
						break;
					}
				case SortBy.Id:
					{
						query = query.OrderByDescending(x => x.Id);
						break;
					}

				case SortBy.PublishAt:
					{
						query = query.OrderByDescending(x => x.PublishAt);
						break;
					}

				case SortBy.Title:
					{
						query = query.OrderByDescending(x => x.Title);
						break;
					}

				case SortBy.TotalComments:
					{
						query = query.OrderByDescending(x => x.TotalComments);
						break;
					}
				case SortBy.TotalTrackback:
					{
						query = query.OrderByDescending(x => x.TotalTrackback);
						break;
					}
				default:
					{
						query = query.OrderByDescending(x => x.PublishAt);
						break;
					}
			}
			
			return query;
		}

		public static IPagedResult<TK> ToPagedResult<T, TK>(this IQueryable<T> query, int currentPage, int pageSize, RavenQueryStatistics stats)
		{
			List<T> data = query.Paging(currentPage, pageSize).ToList();

			if (stats.TotalResults < 1)
			{
				return new EmptyPagedResult<TK>(currentPage, pageSize);
			}

			List<TK> posts = data.MapTo<TK>();

			return new PagedResult<TK>(currentPage, pageSize, posts , stats.TotalResults);
		}

		#endregion
	}
}