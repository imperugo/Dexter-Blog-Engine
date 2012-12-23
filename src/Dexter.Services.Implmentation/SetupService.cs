#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SetupService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/23
// Last edit:	2012/12/23
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Implmentation
{
	using System.Web.Security;

	using Dexter.Data;
	using Dexter.Entities;

	public class SetupService : ISetupService
	{
		#region Fields

		private readonly IConfigurationDataService configurationDataService;

		#endregion

		#region Constructors and Destructors

		public SetupService(IConfigurationDataService configurationDataService)
		{
			this.configurationDataService = configurationDataService;
		}

		#endregion

		#region Public Properties

		public bool IsInstalled
		{
			get
			{
				BlogConfigurationDto configuration = this.configurationDataService.GetConfiguration();

				return configuration != null;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void Initialize(Setup item)
		{
			BlogConfigurationDto configuration = new BlogConfigurationDto(item.BlogName, item.SiteDomain);

			this.configurationDataService.SaveConfiguration(configuration);

			//Membership.CreateUser(item.AdminUsername, item.AdminPassword, item.Email.Address);
			//Roles.CreateRole("Administrator");

			//Roles.AddUserToRole(item.AdminUsername, "Administrator");
		}

		#endregion
	}
}