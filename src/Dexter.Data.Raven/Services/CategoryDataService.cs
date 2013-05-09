#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/01
// Last edit:	2013/03/23
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Dexter.Shared.Exceptions;

	using global::AutoMapper;

	using Common.Logging;

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Helpers;
	using Dexter.Data.Raven.Indexes.Updating;
	using Dexter.Data.Raven.Session;
	using Dexter.Shared.Dto;

	using global::Raven.Abstractions.Extensions;
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

		public void DeleteCategory(int id, int newCategoryId)
		{
			if (id < 1)
			{
				throw new ArgumentException("Category id cannot be lesser than 1.","id");
			}

			if (newCategoryId < 1)
			{
				throw new ArgumentException("New category id cannot be lesser than 1", "newCategoryId");
			}

			var ravenCategoryId = RavenIdHelper.Resolve<Category>(id);
			var ravenNewCategoryId = RavenIdHelper.Resolve<Category>(newCategoryId);

			Category[] categories = this.Session.Load<Category>(new[] { ravenCategoryId, ravenNewCategoryId });

			if (categories[0] == null)
			{
				throw new DexterCategoryNotFoundException(id);
			}

			if (categories[0] == null)
			{
				throw new DexterCategoryNotFoundException(newCategoryId);
			}

			if (categories[0].IsDefault)
			{
				categories[1].IsDefault = true;
			}

			categories[1].ChildrenIds = categories[0].ChildrenIds;

			this.Session.Delete(categories[0]);
			this.Session.Store(categories[1]);

			UpdateCategoryIndex.UpdateCategoryIndexAfterDelete(this.store, this.Session, categories[0], categories[1]);
			UpdateCategoryIndex.UpdateCategoryIndexes(this.store, this.Session, categories[1], categories[0].Name);
		}

		public IList<CategoryDto> GetCategoriesStructure()
		{
			// TODO:http://ayende.com/blog/4801/leaving-the-relational-mindset-ravendbs-trees
			List<Category> cats = this.Session
			                          .Query<Category>()
			                          .Include(x => x.ParentId)
			                          .Select(c => c)
			                          .ToList();

			List<CategoryDto> result = this.MakeTree(cats, null);

			return result;
		}

		public string SaveOrUpdate(CategoryDto item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", "Category item cannot be null.");
			}

			if (item.Name == null)
			{
				throw new ArgumentNullException("item.Name", "Category name cannot be null.");
			}

			if (item.Name == string.Empty)
			{
				throw new ArgumentException("Category name cannot be empty", "item.Name");
			}

			Category category;
			string oldCategoryName = null;

			if (item.Id > 0)
			{
				category = this.Session.Load<Category>(item.Id);
				oldCategoryName = category.Name;

				if (category == null)
				{
					throw new DexterCategoryNotFoundException(item.Id);
				}
			}
			else
			{
				category = new Category();
			}

			item.MapPropertiesToInstance(category);
			category.ParentId = category.ParentId;

			if (string.IsNullOrEmpty(category.Slug))
			{
				category.Slug = SlugHelper.GenerateSlug(category.Name, category.Id, this.GetCategoryBySlug);
			}

			this.Session.Store(category);

			UpdateCategoryIndex.UpdateCategoryIndexes(this.store, this.Session, category, oldCategoryName);

			return category.Id;
		}

		#endregion

		#region Methods

		private Category GetCategoryBySlug(string slug)
		{
			if (slug == null)
			{
				throw new ArgumentNullException("slug");
			}

			if (slug == string.Empty)
			{
				throw new ArgumentException("The string must have a value.", "slug");
			}

			return this.Session.Query<Category>().FirstOrDefault(x => x.Slug == slug);
		}

		private List<CategoryDto> MakeTree(IList<Category> flattenedCategories, string parentId)
		{
			List<CategoryDto> cats = new List<CategoryDto>();

			var filteredFlatCategories = flattenedCategories.Where(fc => fc.ParentId == parentId);

			foreach (var flattenedCat in filteredFlatCategories)
			{
				CategoryDto cat = flattenedCat.MapTo<CategoryDto>();

				IList<CategoryDto> childCats = this.MakeTree(flattenedCategories, flattenedCat.Id);

				cat.Categories.AddRange(childCats);

				foreach (var childCat in childCats)
				{
					childCat.Parent = cat;
				}

				cats.Add(cat);
			}

			return cats;
		}

		#endregion
	}
}