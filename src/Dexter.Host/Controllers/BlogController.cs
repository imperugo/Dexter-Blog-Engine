#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BlogController.cs
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

namespace Dexter.Host.Controllers
{
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.Host.Model.HomeController;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers;
	using Dexter.Web.Core.Filters;

	public class BlogController : DexterControllerBase
	{
		private readonly IPostService postService;

		#region Constructors and Destructors

		public BlogController(ILog logger, IConfigurationService configurationService, IPostService postService)
			: base(logger, configurationService)
		{
			this.postService = postService;
		}

		#endregion

		#region Public Methods and Operators

		[PingBack]
		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public ActionResult Archive(int? year, int? month, int page = 1)
		{
			if (year == null || month == null)
			{
				this.Logger.WarnFormat("Possible wrong link : {0} - The referref is {1}", this.HttpContext.Request.Url, this.HttpContext.Request.UrlReferrer);

				return this.HttpNotFound();
			}

			if (year < 1900 || month < 1)
			{
				return this.HttpNotFound();
			}

			ArchiveViewModel model = new ArchiveViewModel();

			model.Posts = this.postService.GetPostsByDate(page, 10, year.Value, month, null, null);

			return this.View(model);
		}

		#endregion
	}
}