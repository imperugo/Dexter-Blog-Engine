#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryDto.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Entities
{
	using System;
	using System.Collections.Generic;

	public class CategoryDto
	{
		#region Constructors and Destructors

		public CategoryDto()
		{
			this.Categories = new List<CategoryDto>();
		}

		#endregion

		#region Public Properties

		public IList<CategoryDto> Categories { get; set; }

		public int Id { get; set; }

		public bool IsDefault { get; set; }

		public string Name { get; set; }

		public Uri FeedBurnerUrl { get; set; }

		public string Slug { get; set; }

		public string Description { get; set; }

		public int PostCount { get; set; }

		public CategoryDto Parent { get; set; }

		public bool IsChild
		{
			get { return Parent != null; }
		}

		#endregion
	}
}