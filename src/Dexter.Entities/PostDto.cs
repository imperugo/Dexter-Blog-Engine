#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostDto.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Entities
{
	using System;
	using System.Collections.Generic;

	public class PostDto
	{
		#region Public Properties

		public string Abstract { get; set; }

		public bool AllowComments { get; set; }

		public string[] Categories { get; set; }

		public string Content { get; set; }

		public DateTimeOffset PublishAt { get; set; }

		public string Slug { get; set; }

		public ICollection<string> Tags { get; set; }

		public string Title { get; set; }

		public int TotalComments { get; set; }

		public int TotalTrackback { get; set; }

		#endregion
	}
}