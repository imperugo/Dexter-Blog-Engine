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
	using System.Web.Mvc;

	using AutoMapper;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
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
		public ActionResult ConfirmDelete(int id)
		{
			if (id < 1)
			{
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "PostNotFound", null).Redirect();
			}

			PostDto post = this.postService.GetPostByKey(id);

			if (post == null)
			{
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "PostNotFound", null).Redirect();
			}

			PostDeleteViewData model = new PostDeleteViewData();
			model.Post = post;

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
				this.Logger.Error("Error during deleting the post", e);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "GenericError", null).Redirect();
			}

			return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "PostDeleted", this.urlBuilder.Admin.Post.List()).Redirect();
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();

			model.Posts = this.postService.GetPosts(1, 10, new ItemQueryFilter
				                                                                          {
					                                                                          MaxPublishAt = DateTimeOffset.Now.AddYears(1).AsMinutes(), 
					                                                                          MinPublishAt = DateTimeOffset.Now.AddYears(-10).AsMinutes(), 
					                                                                          Status = null
				                                                                          });

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Manage(int? id, int? month, int? day, int? year)
		{
			IList<CategoryDto> categories = this.categoryService.GetCategories();

			PostBinder post = id.HasValue
				                  ? (this.postService.GetPostByKey(id.Value)).MapTo<PostBinder>()
				                  : PostBinder.EmptyInstance();

			ManageViewModel model = new ManageViewModel();

			model.Post = post;
			model.Categories = categories;

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Manage(PostBinder post)
		{
			if (!this.ModelState.IsValid)
			{
				ManageViewModel model = new ManageViewModel();
				model.Post = post;
				model.Categories = this.categoryService.GetCategories();

				return this.View(model);
			}

			try
			{
				this.postService.SaveOrUpdate(post.MapTo<PostDto>());
			}
			catch (Exception e)
			{
				this.Logger.Error("Error during saving the post", e);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "UnableToSaveThePost", null).Redirect();
			}

			return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "PostSaved", this.urlBuilder.Admin.Post.List()).Redirect();
		}

		#endregion
	}
}