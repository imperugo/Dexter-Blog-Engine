#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ItemQueryFilter.cs
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

namespace Dexter.Entities.Filters
{
	using System;

	public class ItemQueryFilter
	{
		public ItemQueryFilter()
		{
			this.OrderFilter = new ItemOrderFilter();
		}

		#region Public Properties

		public DateTimeOffset? MaxPublishAt { get; set; }

		public DateTimeOffset? MinPublishAt { get; set; }

		public ItemStatus? Status { get; set; }

		public ItemOrderFilter OrderFilter { get; set; }

		#endregion
	}

	public class ItemOrderFilter
	{
		public ItemOrderFilter()
		{
			this.SortBy = SortBy.PublishAt;
		}

		public SortBy SortBy { get; set; }
		public bool Ascending { get; set; }
		public bool RandomOrder { get; set; }
	}

	public enum SortBy
	{
		Id,
		Title,
		TotalComments,
		TotalTrackback,
		CreatedAt,
		PublishAt
	}
}