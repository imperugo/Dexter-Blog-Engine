#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			FastPostBinder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/24
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Home
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class FastPostBinder
	{
		#region Public Properties

		public string Abstract { get; set; }

		public bool BreakOnAggregate { get; set; }

		public int? CategoryId { get; set; }

		public bool CommentEnabled { get; set; }

		[AllowHtml]
		[Required]
		public string FormattedBody { get; set; }

		public int Id { get; set; }

		public bool Publish { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
		public DateTime PublishDate { get; set; }

		public int PublishHour { get; set; }

		public int PublishMinutes { get; set; }

		public string Slug { get; set; }

		[Required]
		public string Tags { get; set; }

		[Required]
		public string Title { get; set; }

		#endregion

		#region Public Methods and Operators

		public static FastPostBinder EmptyInstance()
		{
			return new FastPostBinder
				       {
					       PublishDate = DateTime.Now, 
					       PublishHour = DateTime.Now.Hour, 
					       PublishMinutes = DateTime.Now.Minute, 
					       Publish = true, 
					       CommentEnabled = true
				       };
		}

		#endregion
	}
}