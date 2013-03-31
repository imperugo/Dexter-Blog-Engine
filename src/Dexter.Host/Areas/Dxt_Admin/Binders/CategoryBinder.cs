#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryBinder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/23
// Last edit:	2013/03/23
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Binders
{
	using System.ComponentModel.DataAnnotations;

	public class CategoryBinder
	{
		#region Public Properties

		[StringLength(250)]
		public string Description { get; set; }

		[StringLength(1000)]
		[RegularExpression(@"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$")]
		public string FeedBurnerUrl { get; set; }

		[Required]
		public string Name { get; set; }

		public bool IsDefault { get; set; }

		public int? ParentId { get; set; }

		#endregion

		#region Public Methods and Operators

		public static CategoryBinder EmptyInstance()
		{
			return new CategoryBinder();
		}

		#endregion
	}
}