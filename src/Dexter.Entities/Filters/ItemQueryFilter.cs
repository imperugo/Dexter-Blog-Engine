namespace Dexter.Entities.Filters
{
	using System;

	using Dexter.Entities;

	public class ItemQueryFilter
	{
		#region Public Properties

		public DateTimeOffset? MaxPublishAt { get; set; }

		public DateTimeOffset? MinPublishAt { get; set; }

		public ItemStatus? Status { get; set; }

		#endregion
	}
}