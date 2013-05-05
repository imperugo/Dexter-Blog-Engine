#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Item.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/27
// Last edit:	2013/05/05
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Domain
{
	using System;
	using System.Collections.Generic;

	using Dexter.Entities;

	public abstract class Item : EntityBase<string>
	{
		public string Title { get; set; }

		public string Slug { get; set; }

		public DateTimeOffset PublishAt { get; set; }

		public string Excerpt { get; set; }

		public string Content { get; set; }

		public string SearchContent { get; set; }

		public ItemStatus Status { get; set; }

		public Uri FeaturedImage { get; set; }

		public string Author { get; set; }

		public bool AllowComments { get; set; }

		public string CommentsId { get; set; }

		public int TotalComments { get; set; }

		public int TotalTrackback { get; set; }

		public string TrackbacksId { get; set; }

		public ICollection<string> Tags { get; set; }
	}
}