namespace Dexter.Entities
{
	using System;

	public class ItemDto
	{
		public int Id { get; set; }

		public string Abstract { get; set; }

		public bool AllowComments { get; set; }

		public string Content { get; set; }

		public DateTimeOffset PublishAt { get; set; }

		public string Slug { get; set; }

		public ItemStatus Status { get; set; }

		public string Title { get; set; }

		public int TotalComments { get; set; }

		public int TotalTrackback { get; set; }
	}
}