#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			WidgetController.cs
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

namespace Dexter.Host.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Host.Model.Widget;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers;

	public class WidgetController : DexterControllerBase
	{
		private readonly IPostService postService;

		private readonly ICommentService commentService;

		#region Constructors and Destructors

		public WidgetController(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService)
			: base(logger, configurationService)
		{
			this.postService = postService;
			this.commentService = commentService;
		}

		#endregion

		#region Public Methods and Operators

		[ChildActionOnly]
		public ActionResult Archive()
		{
			IList<MonthDto> months = this.postService.GetMonthsForPublishedPosts();

			return this.View(new ArchiveViewModel
				                 {
					                 Months = months
				                 });
		}

		[ChildActionOnly]
		public ActionResult FuturePosts(int maxItemsNumber)
		{
			IPagedResult<PostDto> items = this.postService.GetPosts(1, maxItemsNumber, new ItemQueryFilter
				                                                                                      {
					                                                                                      MinPublishAt = DateTimeOffset.Now.AsMinutes(), 
					                                                                                      MaxPublishAt = DateTimeOffset.Now.AddYears(5).AsMinutes(), 
					                                                                                      Status = ItemStatus.Scheduled
				                                                                                      });

			FuturePostsViewModel model = new FuturePostsViewModel
				                             {
					                             Posts = items
				                             };

			return this.View(model);
		}

		[ChildActionOnly]
		public ActionResult RecentComments(int maxNumberOfComments)
		{
			IList<CommentDto> comments = this.commentService.GetRecentComments(maxNumberOfComments);

			return this.View(new RecentCommentsViewModel
				                 {
					                 Comments = comments
				                 });
		}

		[ChildActionOnly]
		public ActionResult TopTags(int maxNumberOfTags)
		{
			IList<TagDto> topTags = this.postService.GetTopTagsForPublishedPosts(maxNumberOfTags);

			return this.View(new TopTagsViewModel
				                 {
					                 Tags = topTags
				                 });
		}

		[ChildActionOnly]
		public ActionResult PostComment(int itemId)
		{
			var comments = this.commentService.GetCommentForSpecificItem(itemId);

			return this.View(new ItemCommentViewModel()
				                 {
					                 Comments = comments
				                 });
		}

		#endregion
	}
}