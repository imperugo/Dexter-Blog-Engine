#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			HomeController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/02
// Last edit:	2012/12/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Setup.Controllers
{
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Host.Areas.Dxt_Setup.Models.Home;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	public class HomeController : DexterControllerBase
	{
		#region Constructors and Destructors

		public HomeController(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService)
			: base(logger, configurationService, postService, commentService)
		{
		}

		#endregion

		#region Public Methods and Operators

		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();

			return this.View(model);
		}

		#endregion
	}
}