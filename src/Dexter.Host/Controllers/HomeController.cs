#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			HomeController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
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
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Host.Model.HomeController;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers;
	using Dexter.Web.Core.Filters;

	public class HomeController : DexterControllerBase
	{
		private readonly IPostService postService;

		#region Constructors and Destructors

		public HomeController(ILog logger, IConfigurationService configurationService, IPostService postService)
			: base(logger, configurationService)
		{
			this.postService = postService;
		}

		#endregion

		#region Public Methods and Operators

		[PingBack]
		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public ActionResult Index(int page = 1)
		{
			IndexViewModel model = new IndexViewModel();

			model.Posts = this.postService.GetPosts(page, this.BlogConfiguration.ReadingConfiguration.NumberOfPostPerPage, new ItemQueryFilter
				                                                                                  {
					                                                                                  MaxPublishAt = DateTimeOffset.Now, 
					                                                                                  Status = ItemStatus.Published
				                                                                                  });

			return this.View(model);
		}

		[PingBack]
		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public ActionResult Post(string id)
		{
			PostViewModel model = new PostViewModel();

			model.Post = this.postService.GetPostBySlug(id);

			return this.View(model);
		}

		#endregion
	}
}