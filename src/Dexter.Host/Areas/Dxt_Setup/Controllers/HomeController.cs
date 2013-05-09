#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			HomeController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/02
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host.Areas.Dxt_Setup.Controllers
{
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using AutoMapper;

	using Common.Logging;

	using Dexter.Shared.Dto;
	using Dexter.Host.Areas.Dxt_Setup.Models;
	using Dexter.Host.Areas.Dxt_Setup.Models.Home;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers;
	using Dexter.Web.Core.Routing;

	public class HomeController : DexterControllerBase
	{
		#region Fields

		private readonly IRoutingService routingService;

		private readonly ISetupService setupService;

		private readonly IUrlBuilder urlBuilder;

		#endregion

		#region Constructors and Destructors

		public HomeController(ILog logger, IConfigurationService configurationService, IUrlBuilder urlBuilder, ISetupService setupService, IRoutingService routingService)
			: base(logger, configurationService)
		{
			this.urlBuilder = urlBuilder;
			this.setupService = setupService;
			this.routingService = routingService;
		}

		#endregion

		#region Public Methods and Operators

		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public async Task<ActionResult> Index(SetupBinder setup)
		{
			if (!this.ModelState.IsValid)
			{
				IndexViewModel model = new IndexViewModel();
				model.Setup = setup;

				return this.View(model);
			}

			await this.setupService.InitializeAsync(setup.MapTo<Setup>());

			this.routingService.UpdateRoutes();

			return this.urlBuilder.Home.Redirect();
		}

		#endregion
	}
}