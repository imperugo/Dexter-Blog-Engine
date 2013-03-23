#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ICategoryService.cs
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

namespace Dexter.Services
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.Services.Events;

	public interface ICategoryService
	{
		#region Public Events

		/// <summary>
		/// This event will raise after to retrieve <see cref="IPagedResult"/> of <see cref="CategoryDto"/>. The event is raised by by the implementation of <see cref="GetCategories"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IList<CategoryDto>>> CategoriesRetrieved;

		/// <summary>
		/// This event will raise before to retrieve <see cref="IPagedResult"/> of <see cref="CategoryDto"/>. The event is raised by by the implementation of <see cref="GetCategories"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithoutParameters<IList<CategoryDto>>> CategoryRetrieving;

		#endregion

		#region Public Methods and Operators

		IList<CategoryDto> GetCategories();

		CategoryDto GetCategoryById(int id);

		void SaveOrUpdate(CategoryDto category);

		#endregion
	}
}