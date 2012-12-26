#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ManageViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/26
// Last edit:	2012/12/26
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Post
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	using Dexter.Entities;
	using Dexter.Host.App_Start.Validators;
	using Dexter.Web.Core.Models;

	public class ManageViewModel : DexterModelBase
	{
		#region Static Fields

		internal static readonly string[] HoursValues = { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };

		internal static readonly string[] MinutesValues =
			{
				"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", 
				"11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", 
				"22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", 
				"33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", 
				"44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", 
				"55", "56", "57", "58", "59"
			};

		#endregion

		#region Public Properties

		public IList<CategoryDto> Categories { get; set; }

		public string[] Hours
		{
			get
			{
				return HoursValues;
			}
		}

		public string[] Minutes
		{
			get
			{
				return MinutesValues;
			}
		}

		public PostBinder Post { get; set; }

		#endregion
	}

	public class PostBinder
	{
		#region Constructors and Destructors

		public PostBinder()
		{
			this.Categories = new Collection<SimplyCategoryBinder>();
		}

		#endregion

		#region Public Properties

		public string Abstract { get; set; }

		public bool BreakOnAggregate { get; set; }

		[SimplyCategoryValidation]
		public ICollection<SimplyCategoryBinder> Categories { get; set; }

		public bool CommentEnabled { get; set; }

		[Required]
		[AllowHtml]
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

		public static PostBinder EmptyInstance()
		{
			return new PostBinder
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

	public class SimplyCategoryBinder
	{
		#region Public Properties

		public int Id { get; set; }

		public string Name { get; set; }

		#endregion
	}
}