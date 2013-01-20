#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			WidgetController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/20
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Controllers
{
	using System;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Host.Areas.Dxt_Admin.Models.Widget;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	public class WidgetController : DexterControllerBase
	{
		#region Fields

		private readonly ICategoryService categoryService;

		private readonly IPostService postService;

		#endregion

		#region Constructors and Destructors

		public WidgetController(ILog logger, IConfigurationService configurationService, IPostService postService, ICategoryService categoryService)
			: base(logger, configurationService)
		{
			this.postService = postService;
			this.categoryService = categoryService;
		}

		#endregion

		#region Public Methods and Operators

		[HttpGet]
		[ChildActionOnly]
		public ActionResult FastPostEditor()
		{
			FastPostEditorViewModel model = new FastPostEditorViewModel();
			model.Categories = this.categoryService.GetCategories();

			return this.View(model);
		}

		[HttpGet]
		[ChildActionOnly]
		public ActionResult PostList(int pageIndex, int pageSize, ItemQueryFilter filter)
		{
			IPagedResult<PostDto> model = this.postService.GetPosts(1, 5, new ItemQueryFilter
				                                                              {
					                                                              MaxPublishAt = DateTimeOffset.Now.AddMonths(1).AsMinutes(), 
					                                                              MinPublishAt = DateTimeOffset.Now.AsMinutes(), 
					                                                              Status = ItemStatus.Scheduled
				                                                              });

			return this.View(model);
		}

		#endregion
	}
}