namespace Dexter.Data.DataTransferObjects
{
	using System;
	using System.Collections.Generic;

	public class PostDto
	{
		public string Title { get; set; }
		public string Slug { get; set; }
		public string Abstract { get; set; }
		public string Content { get; set; }
		public DateTimeOffset PublishAt { get; set; }
		public bool AllowComments { get; set; }
		public int TotalComments { get; set; }
		public int TotalTrackback { get; set; }
		public ICollection<string> Tags { get; set; }
		public string[] Categories { get; set; }
	}
}