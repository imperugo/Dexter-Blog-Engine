#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			QueryExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Extensions
{
	using System.Linq;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities.Filters;

	using global::Raven.Client.Linq;

	internal static class QueryExtensions
	{
		#region Public Methods and Operators

		public static IRavenQueryable<T> ApplyFilterItem<T>(this IRavenQueryable<T> query, ItemQueryFilter filters) where T : Item
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

		#endregion
	}
}