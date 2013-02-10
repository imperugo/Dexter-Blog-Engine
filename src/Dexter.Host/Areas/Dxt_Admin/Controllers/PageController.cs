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
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Host.Areas.Dxt_Admin.Models.Page;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	[Authorize]
	public class PageController : DexterControllerBase
	{
		private readonly IPageService pageService;

		#region Constructors and Destructors

		public PageController(ILog logger, IConfigurationService configurationService, IPageService pageService)
			: base(logger, configurationService)
		{
			this.pageService = pageService;
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();

			return this.View(model);
		}

		#endregion
	}
}