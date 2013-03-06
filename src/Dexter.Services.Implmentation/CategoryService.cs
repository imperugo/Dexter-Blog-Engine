#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/26
// Last edit:	2013/01/20
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
	using System.Threading.Tasks;

	using Dexter.Data;
	using Dexter.Entities;
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

		public void SaveOrUpdate(CategoryDto category)
		{
			if (category == null)
			{
				throw new ArgumentNullException("category", "The specified category must contain a valid instance");
			}

			if (category.Name == null)
			{
				throw new ArgumentNullException("category.Name", "The category name cannot be null.");
			}

			if (category.Name == string.Empty)
			{
				throw new ArgumentException("category.Name", "The category name cannot be empty.");
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}