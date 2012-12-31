#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ErrorController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/31
// Last edit:	2012/12/31
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Controllers
{
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Host.Areas.Dxt_Admin.Models.Error;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;
	using Dexter.Web.Core.Models;

	public class ErrorController : DexterControllerBase
	{
		#region Constructors and Destructors

		public ErrorController(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService)
			: base(logger, configurationService, postService, commentService)
		{
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult NotFound()
		{
			DexterModelBase model = new DexterModelBase();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult BadRequest()
		{
			DexterModelBase model = new DexterModelBase();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Unauthorized()
		{
			DexterModelBase model = new DexterModelBase();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Forbidden()
		{
			DexterModelBase model = new DexterModelBase();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult ServiceUnavailable()
		{
			DexterModelBase model = new DexterModelBase();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult InternalServerError()
		{
			IndexViewModel model = new IndexViewModel();

			if (this.BlogConfiguration.DebugInfo.ShowException)
			{
				model.Exception = this.Server.GetLastError();
				return this.View("InternalServerErrorDetailed", model);
			}

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult ReportSent()
		{
			//TODO: add binder and email to the notification service

			DexterModelBase model = new DexterModelBase();

			return this.View(model);
		}

		#endregion
	}
}