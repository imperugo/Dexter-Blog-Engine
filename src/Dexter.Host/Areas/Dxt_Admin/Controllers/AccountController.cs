#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AccountController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/26
// Last edit:	2013/04/27
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Controllers
{
	using System.Linq;
	using System.Web.Mvc;
	using System.Web.Security;

	using AutoMapper;

	using Common.Logging;

	using Dexter.Dependency.Extensions;
	using Dexter.Entities.Extensions;
	using Dexter.Host.Areas.Dxt_Admin.Models.Account;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Shared;
	using Dexter.Web.Core.Controllers;

	[Authorize(Roles = Constants.AdministratorRole)]
	public class AccountController : DexterControllerBase
	{
		private IUrlBuilder urlBuilder;

		#region Constructors and Destructors

		public AccountController(ILog logger, IConfigurationService configurationService, IUrlBuilder urlBuilder)
			: base(logger, configurationService)
		{
			this.urlBuilder = urlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		[HttpGet]
		public ActionResult Index(int pageIndex = 0, int pageSize = 30)
		{
			int numberOfUsers;
			MembershipUserCollection users = Membership.GetAllUsers(pageIndex, pageSize, out numberOfUsers);

			IndexViewModel model = new IndexViewModel();
			model.Users = users.Cast<MembershipUser>()
			                   .AsEnumerable()
			                   .MapTo<User>()
			                   .ToPagedResult(pageIndex, pageSize, numberOfUsers);

			model.Users.Result.ForEach(u => u.Roles = Roles.GetRolesForUser(u.Username).ToList());

			return this.View(model);
		}

		[HttpGet]
		public ActionResult ConfirmDelete(string id)
		{
			var user = Membership.GetUser(id);

			if (user == null)
			{
				return this.HttpNotFound();
			}

			DetailViewModel model = new DetailViewModel();
			model.User = user.MapTo<User>();
			model.User.Roles = Roles.GetRolesForUser(id).ToList();

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(string id)
		{
			var administrators = Roles.GetUsersInRole(Constants.AdministratorRole);

			if (administrators.Count() < 2)
			{
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "CannotDeleteTheOnlyAdministrator", null).Redirect();
			}

			Membership.DeleteUser(id);

			if (User.Identity.Name == id)
			{
				return this.urlBuilder.Admin.Logout().Redirect();
			}

			return this.urlBuilder.Admin.Account.List().Redirect();
		}

		#endregion
	}
}