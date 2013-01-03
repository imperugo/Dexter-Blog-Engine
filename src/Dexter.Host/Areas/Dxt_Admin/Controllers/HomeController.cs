#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			HomeController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/24
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Host.Areas.Dxt_Admin.Models.Home;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	[Authorize]
	public class HomeController : DexterControllerBase
	{
		private readonly IPostService postService;

		private readonly ICategoryService categoryService;

		#region Constructors and Destructors

		public HomeController(ILog logger, IConfigurationService configurationService, IPostService postService, ICategoryService categoryService)
			: base(logger, configurationService)
		{
			this.postService = postService;
			this.categoryService = categoryService;
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> Index()
		{
			Task<IPagedResult<PostDto>> futurePosts = this.postService.GetPostsAsync(1, 5, new ItemQueryFilter
				                                                                               {
					                                                                               MaxPublishAt = DateTimeOffset.Now.AddMonths(1).AsMinutes(), 
					                                                                               MinPublishAt = DateTimeOffset.Now.AsMinutes(), 
					                                                                               Status = ItemStatus.Scheduled
				                                                                               });

			Task<IList<CategoryDto>> categories = this.categoryService.GetCategoriesAsync();

			await Task.WhenAll(futurePosts, categories);

			IndexViewModel model = new IndexViewModel();
			model.FuturePosts = futurePosts.Result;
			model.Categories = categories.Result;

			return this.View(model);
		}

		#endregion
	}
}