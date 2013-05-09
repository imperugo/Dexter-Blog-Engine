namespace Dexter.Host.Areas.Dxt_Admin.Binders
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	using Dexter.Shared.Dto;

	public abstract class ItemBinder
	{
		public string Excerpt { get; set; }

		public bool AllowComments { get; set; }

		[Required]
		[AllowHtml]
		public string FormattedBody { get; set; }

		public int Id { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
		public DateTimeOffset PublishAt { get; set; }

		public int PublishHour { get; set; }

		public int PublishMinutes { get; set; }

		public string Slug { get; set; }

		[Required]
		public string Tags { get; set; }

		[Required]
		public string Title { get; set; }

		public ItemStatus Status { get; set; }
	}
}