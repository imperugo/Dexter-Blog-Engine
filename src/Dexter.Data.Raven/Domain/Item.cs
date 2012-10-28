#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Item.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
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

	public abstract class Item : EntityBase
	{
		#region Public Properties

		public bool AllowComments { get; set; }

		public int CommentsId { get; set; }

		public string Content { get; set; }

		public string Excerpt { get; set; }

		public DateTimeOffset PublishAt { get; set; }

		public string Slug { get; set; }

		public ItemStatus Status { get; set; }

		public string Title { get; set; }

		public int TotalComments { get; set; }

		public int TotalTrackback { get; set; }

		#endregion
	}
}