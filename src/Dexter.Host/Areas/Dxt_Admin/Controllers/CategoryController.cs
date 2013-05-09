#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/24
// Last edit:	2013/03/24
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Controllers
{
	using System;
	using System.Linq;
	using System.Web.Mvc;

	using AutoMapper;

	using Common.Logging;

	using Dexter.Shared.Dto;
	using Dexter.Host.Areas.Dxt_Admin.Binders;
	using Dexter.Host.Areas.Dxt_Admin.Models.Category;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Shared.Exceptions;
	using Dexter.Web.Core.Controllers;

	[Authorize]
	public class CategoryController : DexterControllerBase
	{
		#region Fields

		private readonly ICategoryService categoryService;

		private readonly IUrlBuilder urlBuilder;

		#endregion

		#region Constructors and Destructors

		public CategoryController(ILog logger, IConfigurationService configurationService, ICategoryService categoryService, IUrlBuilder urlBuilder)
			: base(logger, configurationService)
		{
			this.categoryService = categoryService;
			this.urlBuilder = urlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		[HttpGet]
		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();
			model.Categories = this.categoryService.GetCategories().ToFlat(x => x.Categories).ToList();

			return this.View(model);
		}

		[HttpGet]
		public ActionResult Manage(int? id)
		{
			if (id.HasValue && id.Value < 1)
			{
				return this.HttpNotFound();
			}

			ManageViewModel model = new ManageViewModel();

			model.Category = id.HasValue && id > 0
				                 ? this.categoryService.GetCategoryById(id.Value).MapTo<CategoryBinder>()
				                 : CategoryBinder.EmptyInstance();

			model.Categories = id.HasValue
				                   ? this.categoryService.GetCategories().Where(x => x.Id != id.Value)
				                   : this.categoryService.GetCategories();

			return this.View(model);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Manage(int? id, CategoryBinder category)
		{
			if (!this.ModelState.IsValid)
			{
				ManageViewModel model = new ManageViewModel();

				model.Category = category;

				model.Categories = id.HasValue
					                   ? this.categoryService.GetCategories().Where(x => x.Id != id.Value)
					                   : this.categoryService.GetCategories();

				return this.View(model);
			}

			try
			{
				CategoryDto newCategory;

				if (id.HasValue && id.Value > 0)
				{
					newCategory = this.categoryService.GetCategoryById(id.Value);
				}
				else
				{
					newCategory = new CategoryDto();
				}

				if (category.ParentId.HasValue)
				{
					newCategory.Parent = this.categoryService.GetCategoryById(category.ParentId.Value);
				}

				if (!string.IsNullOrEmpty(category.FeedBurnerUrl))
				{
					newCategory.FeedBurnerUrl = new Uri(category.FeedBurnerUrl);
				}

				newCategory.Name = category.Name;
				newCategory.Description = category.Description;
				newCategory.IsDefault = category.IsDefault;

				this.categoryService.SaveOrUpdate(newCategory);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "Category Saved", this.urlBuilder.Admin.Category.List()).Redirect();
			}
			catch (DexterException e)
			{
				this.Logger.ErrorFormat("Unable to save the specified category", e);
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "Category Not Saved", this.urlBuilder.Admin.Category.List()).Redirect();
			}
		}

		[HttpGet]
		public ActionResult ConfirmDelete(int id)
		{
			if (id < 1)
			{
				return this.HttpNotFound();
			}

			ConfirmDeleteViewModel model = new ConfirmDeleteViewModel();
			model.Category = this.categoryService.GetCategoryById(id);
			model.Categories = this.categoryService.GetCategories().Where(x => x.Id != id);

			return this.View(model);
		}

		[HttpPost]
		public ActionResult Delete(int id, int? newCategoryId)
		{
			try
			{
				this.categoryService.Delete(id, newCategoryId);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "Category Deleted", this.urlBuilder.Admin.Category.List()).Redirect();
			}
			catch (DexterException e)
			{
				this.Logger.ErrorFormat("Unable to save the specified category", e);
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "Category Not Deleted", this.urlBuilder.Admin.Category.List()).Redirect();
			}
		}

		#endregion
	}
}