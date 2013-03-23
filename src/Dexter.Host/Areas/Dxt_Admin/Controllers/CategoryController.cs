#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/24
// Last edit:	2013/03/23
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Controllers
{
	using System.Linq;
	using System.Web.Mvc;

	using AutoMapper;

	using Common.Logging;

	using Dexter.Host.Areas.Dxt_Admin.Binders;
	using Dexter.Host.Areas.Dxt_Admin.Models.Category;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	[Authorize]
	public class CategoryController : DexterControllerBase
	{
		#region Fields

		private readonly ICategoryService categoryService;

		#endregion

		#region Constructors and Destructors

		public CategoryController(ILog logger, IConfigurationService configurationService, ICategoryService categoryService)
			: base(logger, configurationService)
		{
			this.categoryService = categoryService;
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();
			model.Categories = this.categoryService.GetCategories().ToFlat(x => x.Categories).ToList();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Manage(int? id)
		{
			ManageViewModel model = new ManageViewModel();

			model.Category = id.HasValue && id > 0
				                 ? this.categoryService.GetCategoryById(id.Value).MapTo<CategoryBinder>()
				                 : CategoryBinder.EmptyInstance();

			model.Categories = id.HasValue
				                   ? this.categoryService.GetCategories().Where(x => x.Id != id.Value)
				                   : this.categoryService.GetCategories();

			return this.View(model);
		}

		#endregion
	}
}