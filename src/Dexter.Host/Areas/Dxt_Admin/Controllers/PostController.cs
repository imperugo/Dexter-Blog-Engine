#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/24
// Last edit:	2012/12/26
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

	using AutoMapper;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Extensions.Logging;
	using Dexter.Host.Areas.Dxt_Admin.Models.Post;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	[Authorize]
	public class PostController : DexterControllerBase
	{
		#region Fields

		private readonly IPostService postService;

		private readonly ICategoryService categoryService;

		private readonly IUrlBuilder urlBuilder;

		#endregion

		#region Constructors and Destructors

		public PostController(ILog logger, IConfigurationService configurationService, IPostService postService, ICategoryService categoryService, IUrlBuilder urlBuilder)
			: base(logger, configurationService)
		{
			this.postService = postService;
			this.categoryService = categoryService;
			this.urlBuilder = urlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> ConfirmDelete(int id)
		{
			if (id < 1)
			{
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "PostNotFound", null).Redirect();
			}

			PostDto post = await this.postService.GetPostByKeyAsync(id);

			if (post == null)
			{
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "PostNotFound", null).Redirect();
			}

			PostDeleteViewData model = new PostDeleteViewData();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(int id)
		{
			if (id < 1)
			{
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "PostNotFound", null).Redirect();
			}

			try
			{
				this.postService.Delete(id);
			}
			catch (Exception e)
			{
				this.Logger.ErrorAsync("Error during deleting the post", e);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "GenericError", null).Redirect();
			}

			return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "PostDeleted", this.urlBuilder.Admin.Post.List()).Redirect();
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> Index()
		{
			IndexViewModel model = new IndexViewModel();

			Task<IPagedResult<PostDto>> posts = this.postService.GetPostsAsync(1, 10, new ItemQueryFilter
				                                                                          {
					                                                                          MaxPublishAt = DateTimeOffset.Now.AddYears(1).AsMinutes(), 
					                                                                          MinPublishAt = DateTimeOffset.Now.AddYears(-10).AsMinutes(), 
					                                                                          Status = null
				                                                                          });

			await Task.WhenAll(posts);

			model.Posts = posts.Result;

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> Manage(int? id, int? month, int? day, int? year)
		{
			Task<IList<CategoryDto>> categories = this.categoryService.GetCategoriesAsync();

			PostBinder post = id.HasValue
				                  ? (await this.postService.GetPostByKeyAsync(id.Value)).MapTo<PostBinder>()
				                  : PostBinder.EmptyInstance();

			ManageViewModel model = new ManageViewModel();

			await Task.WhenAll(categories);

			model.Post = post;
			model.Categories = categories.Result;

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public async Task<ActionResult> Manage(PostBinder post)
		{
			if (!this.ModelState.IsValid)
			{
				ManageViewModel model = new ManageViewModel();
				model.Post = post;
				model.Categories = await this.categoryService.GetCategoriesAsync();

				return this.View(model);
			}

			try
			{
				this.postService.SaveOrUpdate(post.MapTo<PostDto>());
			}
			catch (Exception e)
			{
				this.Logger.Error("Error during saving the post", e);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "GenericError", null).Redirect();
			}

			return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "PostSaved", this.urlBuilder.Admin.Post.List()).Redirect();
		}

		#endregion
	}
}