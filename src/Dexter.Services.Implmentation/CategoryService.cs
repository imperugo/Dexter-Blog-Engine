#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/26
// Last edit:	2013/03/31
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Implmentation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Dexter.Data;
	using Dexter.Shared.Dto;
	using Dexter.Services.Events;

	public class CategoryService : ICategoryService
	{
		#region Fields

		private readonly ICategoryDataService categoryDataService;

		#endregion

		#region Constructors and Destructors

		public CategoryService(ICategoryDataService categoryDataService)
		{
			this.categoryDataService = categoryDataService;
		}

		#endregion

		#region Public Events

		public event EventHandler<GenericEventArgs<IList<CategoryDto>>> CategoriesRetrieved;

		public event EventHandler<CancelEventArgsWithoutParameters<IList<CategoryDto>>> CategoryRetrieving;

		#endregion

		#region Public Methods and Operators

		public void Delete(int categoryId, int? newCategoryId)
		{
			if (!newCategoryId.HasValue)
			{
				newCategoryId = this.GetCategories().Single(x => x.IsDefault).Id;
			}

			this.categoryDataService.DeleteCategory(categoryId, newCategoryId.Value);
		}

		public IList<CategoryDto> GetCategories()
		{
			CancelEventArgsWithoutParameters<IList<CategoryDto>> e = new CancelEventArgsWithoutParameters<IList<CategoryDto>>(null);

			this.CategoryRetrieving.Raise(this, e);

			if (e.Cancel)
			{
				return e.Result;
			}

			IList<CategoryDto> data = this.categoryDataService.GetCategoriesStructure();

			this.CategoriesRetrieved.Raise(this, new GenericEventArgs<IList<CategoryDto>>(data));

			return data;
		}

		public CategoryDto GetCategoryById(int id)
		{
			return this.GetFlatCategories().FirstOrDefault(x => x.Id == id);
		}

		public IList<CategoryDto> GetFlatCategories()
		{
			return this.GetCategories().ToFlat(x => x.Categories).ToList();
		}

		public void SaveOrUpdate(CategoryDto category)
		{
			this.categoryDataService.SaveOrUpdate(category);
		}

		#endregion
	}
}