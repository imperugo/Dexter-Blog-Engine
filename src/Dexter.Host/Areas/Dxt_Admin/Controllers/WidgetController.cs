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

	using Dexter.Shared.Dto;
	using Dexter.Host.Areas.Dxt_Admin.Models.Widget;
	using Dexter.Services;
	using Dexter.Shared.Filters;
	using Dexter.Shared.Result;
	using Dexter.Web.Core.Controllers;

	public class WidgetController : DexterControllerBase
	{
		#region Fields

		private readonly ICategoryService categoryService;

		private readonly IPostService postService;

		private readonly IPageService pageService;

		#endregion

		#region Constructors and Destructors

		public WidgetController(ILog logger, IConfigurationService configurationService, IPostService postService, ICategoryService categoryService, IPageService pageService)
			: base(logger, configurationService)
		{
			this.postService = postService;
			this.categoryService = categoryService;
			this.pageService = pageService;
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
			if (filter == null)
			{
				filter = new ItemQueryFilter
					         {
						         MaxPublishAt = DateTimeOffset.Now.AddMonths(1).AsMinutes(),
						         MinPublishAt = DateTimeOffset.Now.AsMinutes(),
						         Status = ItemStatus.Scheduled
					         };
			}
			IPagedResult<PostDto> model = this.postService.GetPosts(1, 5, filter);

			return this.View(model);
		}

		[HttpGet]
		[ChildActionOnly]
		public ActionResult PageList(int pageIndex, int pageSize, ItemQueryFilter filter)
		{
			if (filter == null)
			{
				filter = new ItemQueryFilter
				{
					MaxPublishAt = DateTimeOffset.Now.AddMonths(1).AsMinutes(),
					MinPublishAt = DateTimeOffset.Now.AsMinutes(),
					Status = ItemStatus.Scheduled
				};
			}

			IPagedResult<PageDto> model = this.pageService.GetPages(1, 5, filter);

			return this.View(model);
		}

		#endregion
	}
}