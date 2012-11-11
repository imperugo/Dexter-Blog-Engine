#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			HomeController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/11
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Controllers
{
	using System;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Host.Model.HomeController;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;
	using Dexter.Web.Core.Filters;

	public class HomeController : DexterControllerBase
	{
		#region Constructors and Destructors

		public HomeController(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService)
			: base(logger, configurationService, postService, commentService)
		{
		}

		#endregion

		#region Public Methods and Operators

		[PingBack]
		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public ActionResult Archive(int? year, int? month, int page = 1)
		{
			if (year == null || month == null)
			{
				this.Logger.WarnFormat("Possible wrong link : {0} - The referref is {1}", this.HttpContext.Request.Url, this.HttpContext.Request.UrlReferrer);

				return this.HttpNotFound();
			}

			if (year < 1900 || month < 1)
			{
				return this.HttpNotFound();
			}

			throw new NotImplementedException("To be completed");
		}

		[PingBack]
		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public async Task<ActionResult> Index(int page = 1)
		{
			IndexViewModel model = new IndexViewModel();

			Task<IPagedResult<PostDto>> postsTasks = this.PostService.GetPostsAsync(page, 10, new ItemQueryFilter
				                                                                                  {
					                                                                                  MaxPublishAt = DateTimeOffset.Now.AsMinutes(), 
					                                                                                  Status = ItemStatus.Published
				                                                                                  });

			await Task.WhenAll(postsTasks);

			model.Posts = postsTasks.Result;

			return await this.Json(model, JsonRequestBehavior.AllowGet);
		}

		[PingBack]
		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public async Task<ActionResult> Post(string id)
		{
			PostViewModel model = new PostViewModel();

			Task<PostDto> postTasks = this.PostService.GetPostBySlugAsync(id);

			await Task.WhenAll(postTasks);

			model.Post = postTasks.Result;

			return await this.View(model);
		}

		#endregion
	}
}