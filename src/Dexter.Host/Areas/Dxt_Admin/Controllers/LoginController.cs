#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LoginController.cs
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
	using System.Web.Security;

	using Common.Logging;

	using Dexter.Host.Areas.Dxt_Admin.Models.Login;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	public class LoginController : DexterControllerBase
	{
		private readonly IUrlBuilder urlBuilder;

		#region Constructors and Destructors

		public LoginController(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService, IUrlBuilder urlBuilder)
			: base(logger, configurationService, postService, commentService)
		{
			this.urlBuilder = urlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();
			model.Credential = new CredentialLoginBinder();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Index(CredentialLoginBinder credential)
		{
			if (!ModelState.IsValid)
			{
				IndexViewModel model = new IndexViewModel();
				model.Credential = credential;

				return this.View(model);
			}

			bool validCredentials = Membership.ValidateUser(credential.Username, credential.Password);

			if (!validCredentials)
			{
				IndexViewModel model = new IndexViewModel();
				model.Credential = credential;

				return this.View(model);
			}

			FormsAuthentication.SetAuthCookie(credential.Username, true);

			return this.urlBuilder.Admin.Home().Redirect();
		}

		#endregion
	}
}