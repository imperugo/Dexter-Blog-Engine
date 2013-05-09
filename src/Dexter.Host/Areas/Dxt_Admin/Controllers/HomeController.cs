#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			HomeController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/24
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
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Shared.Dto;
	using Dexter.Host.Areas.Dxt_Admin.Models.Home;
	using Dexter.Services;
	using Dexter.Shared.Filters;
	using Dexter.Web.Core.Controllers;

	[Authorize]
	public class HomeController : DexterControllerBase
	{
		private readonly IPostService postService;

		#region Fields

		#endregion

		#region Constructors and Destructors

		public HomeController(ILog logger, IConfigurationService configurationService, IPostService postService)
			: base(logger, configurationService)
		{
			this.postService = postService;
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();

			model.FuturePosts = this.postService.GetPosts(1, 50, new ItemQueryFilter()
				                                                        {
																			MaxPublishAt = DateTimeOffset.Now.AddDays(7).AsMinutes(),
																			MinPublishAt = DateTimeOffset.Now.AsMinutes(),
																			Status = ItemStatus.Scheduled
				                                                        });

			return this.View(model);
		}

		#endregion
	}
}