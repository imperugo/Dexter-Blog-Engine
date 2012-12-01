#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/01
// Last edit:	2012/12/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Services
{
	using System;

	using Common.Logging;

	using Dexter.Data.Exceptions;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Indexes.Updating;
	using Dexter.Data.Raven.Session;

	using global::Raven.Client;

	public class CategoryDataService : ServiceBase, ICategoryDataService
	{
		#region Fields

		private readonly IDocumentStore store;

		#endregion

		#region Constructors and Destructors

		public CategoryDataService(ILog logger, ISessionFactory sessionFactory, IDocumentStore store)
			: base(logger, sessionFactory)
		{
			this.store = store;
		}

		#endregion

		#region Public Methods and Operators

		public void DeleteCategory(string id, string newCategoryId)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id", "Category id cannot be null.");
			}

			if (id == string.Empty)
			{
				throw new ArgumentException("Category id cannot be empty", "id");
			}

			if (newCategoryId == null)
			{
				throw new ArgumentNullException("newCategoryId", "New category id cannot be null.");
			}

			if (newCategoryId == string.Empty)
			{
				throw new ArgumentException("New category id cannot be empty", "newCategoryId");
			}

			Category[] categories = this.Session.Load<Category>(new[] { id, newCategoryId });

			if (categories[0] == null)
			{
				throw new CategoryNotFoundException(id);
			}

			if (categories[0] == null)
			{
				throw new CategoryNotFoundException(newCategoryId);
			}

			if (categories[0].IsDefault)
			{
				categories[1].IsDefault = true;
			}

			this.Session.Delete(categories[0]);
			this.Session.Store(categories[1]);

			UpdateCategoryIndex.UpdateCategoryIndexAfterDelete(this.store, this.Session, categories[0], categories[1]);
		}

		public string SaveOrUpdate(string name, bool isDefault, string parentCategoryId, string categoryId = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "Category name cannot be null.");
			}

			if (name == string.Empty)
			{
				throw new ArgumentException("Category name cannot be empty", "name");
			}

			Category category;

			if (categoryId != null)
			{
				category = this.Session.Load<Category>(categoryId);

				if (category == null)
				{
					throw new CategoryNotFoundException(categoryId);
				}
			}
			else
			{
				category = new Category();
				category.Name = name;
			}

			category.IsDefault = isDefault;
			category.ParentId = parentCategoryId;

			this.Session.Store(category);

			UpdateCategoryIndex.UpdateCategoryIndexes(this.store, this.Session, category);

			return category.Id;
		}

		#endregion
	}
}