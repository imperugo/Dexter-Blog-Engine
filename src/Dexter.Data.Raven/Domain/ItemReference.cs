namespace Dexter.Data.Raven.Domain
{
	using System;

	using Dexter.Entities;

	public class ItemReference
	{
		public int Id { get; set; }

		public ItemStatus Status { get; set; }

		public DateTimeOffset ItemPublishedAt { get; set; }

		public static implicit operator ItemReference(Item item)
		{
			return new ItemReference
			{
				Id = item.Id,
				Status = item.Status,
				ItemPublishedAt = item.PublishAt
			};
		}
	}
}