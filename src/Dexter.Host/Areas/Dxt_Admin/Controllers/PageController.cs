#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PageController.cs
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
	using System.Web.Mvc;

	using AutoMapper;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Host.Areas.Dxt_Admin.Binders;
	using Dexter.Host.Areas.Dxt_Admin.Models.Page;
	using Dexter.Host.Areas.Dxt_Admin.Models.Post;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	using IndexViewModel = Dexter.Host.Areas.Dxt_Admin.Models.Page.IndexViewModel;
	using ManageViewModel = Dexter.Host.Areas.Dxt_Admin.Models.Page.ManageViewModel;

	[Authorize]
	public class PageController : DexterControllerBase
	{
		#region Fields

		private readonly IPageService pageService;

		private readonly IUrlBuilder urlBuilder;

		#endregion

		#region Constructors and Destructors

		public PageController(ILog logger, IConfigurationService configurationService, IPageService pageService, IUrlBuilder urlBuilder)
			: base(logger, configurationService)
		{
			this.pageService = pageService;
			this.urlBuilder = urlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult ConfirmDelete(int id)
		{
			if (id < 1)
			{
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "PageNotFound", null).Redirect();
			}

			PageDto page = this.pageService.GetPageByKey(id);

			if (page == null)
			{
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "PageNotFound", null).Redirect();
			}

			PageDeleteViewData model = new PageDeleteViewData();
			model.Page = page;

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(int id)
		{
			if (id < 1)
			{
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "PageNotFound", null).Redirect();
			}

			try
			{
				this.pageService.Delete(id);
			}
			catch (Exception e)
			{
				this.Logger.Error("Error during deleting the page", e);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "GenericError", null).Redirect();
			}

			return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "PageDeleted", this.urlBuilder.Admin.Post.List()).Redirect();
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();

			model.Pages = this.pageService.GetPages(1, 10, new ItemQueryFilter
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
			PageBinder post = id.HasValue
								  ? (this.pageService.GetPageByKey(id.Value)).MapTo<PageBinder>()
								  : PageBinder.EmptyInstance();

			ManageViewModel model = new ManageViewModel();

			model.Page = post;

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Manage(PageBinder page)
		{
			if (!this.ModelState.IsValid)
			{
				ManageViewModel model = new ManageViewModel();
				model.Page = page;

				return this.View(model);
			}

			try
			{
				this.pageService.SaveOrUpdate(page.MapTo<PageDto>());
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