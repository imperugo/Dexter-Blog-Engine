#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ItemRequest.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/09
// Last edit:	2013/05/09
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Requests
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using Dexter.Shared.Dto;

	public abstract class ItemRequest
	{
		[Range(0, int.MaxValue)]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Slug { get; set; }

		public ItemStatus Status { get; set; }

		public string Excerpt { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public DateTimeOffset CreatedAt { get; set; }

		[Required]
		public DateTimeOffset PublishAt { get; set; }

		public string Author { get; set; }

		public bool AllowComments { get; set; }

		public Uri FeaturedImage { get; set; }

		[Required]
		[MaxLength(1)]
		public string[] Tags { get; set; }

		public bool IsTransient
		{
			get
			{
				return this.Id == 0;
			}
		}
	}
}