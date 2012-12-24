﻿#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/01
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
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

	using global::AutoMapper;

	using Common.Logging;

	using Dexter.Data.Exceptions;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Indexes.Updating;
	using Dexter.Data.Raven.Session;
	using Dexter.Entities;

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

		// public List<CategoryDto> MakeTreeFromFlatList(IList<CategoryDto> flatList)
		// {
		// 	var dic = flatList.ToDictionary(n => n.Id, n => n);

		// 	var rootNodes = new List<CategoryDto>();

		// 	foreach (CategoryDto node in flatList)
		// 	{
		// 		if (node.ParentId != null)
		// 		{
		// 			CategoryDto parent = dic[node.Parent.Id].MapTo<CategoryDto>();
		// 			node.Parent = parent;
		// 			parent.Categories.Add(node);
		// 		}
		// 		else
		// 		{
		// 			rootNodes.Add(node);
		// 		}
		// 	}
		// 	return rootNodes;
		// }
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

			categories[1].ChildrenIds = categories[0].ChildrenIds;

			this.Session.Delete(categories[0]);
			this.Session.Store(categories[1]);

			UpdateCategoryIndex.UpdateCategoryIndexAfterDelete(this.store, this.Session, categories[0], categories[1]);
			UpdateCategoryIndex.UpdateCategoryIndexes(this.store, this.Session, categories[1]);
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

		#region Methods

		private List<CategoryDto> MakeTree(List<Category> flatObjects, CategoryDto parent)
		{
			if (parent == null)
			{
				parent = new CategoryDto();
			}

			return flatObjects
				.Where(x => x.ParentId == parent.Id)
				.Select(item => new CategoryDto
					                {
						                Id = item.Id, 
						                Categories = this.MakeTree(flatObjects, item.MapTo<CategoryDto>())
					                }).ToList();
		}

		#endregion
	}
}