#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterControllerBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Controllers.Web
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Services;
	using Dexter.Web.Core.Models;

	public class DexterControllerBase : AsyncController
	{
		#region Fields

		private readonly ICommentService commentService;

		private readonly IConfigurationService configurationService;

		private readonly ILog logger;

		private readonly IPostService postService;

		#endregion

		#region Constructors and Destructors

		public DexterControllerBase(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService)
		{
			this.logger = logger;
			this.configurationService = configurationService;
			this.postService = postService;
			this.commentService = commentService;
		}

		#endregion

		#region Public Properties

		public IConfigurationService ConfigurationService
		{
			get
			{
				return this.configurationService;
			}
		}

		public ILog Logger
		{
			get
			{
				return this.logger;
			}
		}

		public ICommentService CommentService
		{
			get
			{
				return this.commentService;
			}
		}

		public IPostService PostService
		{
			get
			{
				return this.postService;
			}
		}

		#endregion

		#region Public Methods and Operators

		public new async Task<ActionResult> Json(object model, JsonRequestBehavior behavior)
		{
			DexterModelBase m = (DexterModelBase)model;

			Task<IPagedResult<PostDto>> recentPosts = this.postService.GetPostsAsync(1, 5, new ItemQueryFilter()
				                                                                               {
					                                                                               Status = ItemStatus.Published
				                                                                               });

			Task<IList<CommentDto>> recentComments = this.commentService.GetRecentCommentsAsync(5);

			await Task.WhenAll(recentPosts, recentComments);

			m.RecentPosts = recentPosts.Result.Result;
			m.RecentComments = recentComments.Result;

			return base.Json(model, behavior);
		}

		public new async Task<ActionResult> View(object model)
		{
			return await this.View(null, model);
		}

		public new async Task<ActionResult> View(string viewName, object model)
		{
			DexterModelBase m = (DexterModelBase)model;

			Task<IPagedResult<PostDto>> recentPosts = this.postService.GetPostsAsync(1, 5);
			Task<IList<CommentDto>> recentComments = this.commentService.GetRecentCommentsAsync(5);

			await Task.WhenAll(recentPosts, recentComments);

			m.RecentPosts = recentPosts.Result.Result;
			m.RecentComments = recentComments.Result;

			return base.View(viewName, model);
		}

		#endregion
	}
}