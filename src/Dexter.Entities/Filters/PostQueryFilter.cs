namespace Dexter.Entities.Filters
{
	using System;

	using Dexter.Entities;

	public class PostQueryFilter
	{
		#region Public Properties

		public DateTimeOffset? MaxPublishAt { get; set; }

		public DateTimeOffset? MinPublishAt { get; set; }

		public PostStatus? Status { get; set; }

		#endregion
	}
}