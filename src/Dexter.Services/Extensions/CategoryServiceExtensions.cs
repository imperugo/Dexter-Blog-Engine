#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryServiceExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/10
// Last edit:	2013/03/10
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Extensions
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Dexter.Entities;

	public static class CategoryServiceExtensions
	{
		#region Public Methods and Operators

		public static Task<IList<CategoryDto>> GetCategoriesAsync(this ICategoryService categoryService)
		{
			return Task.Factory.StartNewDexterTask(() => categoryService.GetCategories());
		}

		public static Task SaveOrUpdateAsync(this ICategoryService categoryService, CategoryDto category)
		{
			return Task.Factory.StartNewDexterTask(() => categoryService.SaveOrUpdate(category));
		}

		#endregion
	}
}