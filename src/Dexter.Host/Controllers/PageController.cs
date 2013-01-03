#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PageController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
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
	using Dexter.Host.Model.PageController;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;
	using Dexter.Web.Core.Filters;

	public class PageController : DexterControllerBase
	{
		#region Fields

		private readonly IPageService pageService;

		#endregion

		#region Constructors and Destructors

		public PageController(ILog logger, IConfigurationService configurationService, IPageService pageService)
			: base(logger, configurationService)
		{
			this.pageService = pageService;
		}

		#endregion

		#region Public Methods and Operators

		[PingBack]
		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public async Task<ActionResult> Index(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return this.HttpNotFound();
			}

			Task<PageDto> pageTasks = this.pageService.GetPageBySlugAsync(id);

			await Task.WhenAll(pageTasks);

			if (pageTasks == null)
			{
				return this.HttpNotFound();
			}

			IndexViewModel model = new IndexViewModel();

			model.Page = pageTasks.Result;

			return this.View(model);
		}

		#endregion
	}
}