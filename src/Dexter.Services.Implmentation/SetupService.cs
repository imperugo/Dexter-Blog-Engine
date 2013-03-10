#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SetupService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/23
// Last edit:	2013/03/04
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Implmentation
{
	using System;
	using System.IO;
	using System.Threading.Tasks;
	using System.Web.Security;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Entities;

	public class SetupService : ISetupService
	{
		#region Fields

		private readonly ICategoryDataService categoryService;

		private readonly IConfigurationDataService configurationDataService;

		private readonly IPostDataService postDataService;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		public SetupService(IConfigurationDataService configurationDataService, IPostDataService postDataService, ICategoryDataService categoryService, ILog logger)
		{
			this.configurationDataService = configurationDataService;
			this.postDataService = postDataService;
			this.categoryService = categoryService;
			this.logger = logger;
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
			string defaultPostPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Setup/defaultPost.dxt");

			BlogConfigurationDto configuration = new BlogConfigurationDto(item.BlogName, item.SiteDomain);

			this.configurationDataService.SaveConfiguration(configuration);
			this.logger.Debug("Created blog configuration.");

			//Creating default category
			this.categoryService.SaveOrUpdate("Various", true, null);
			this.logger.Debug("Created default category.");

			// Creating user
			Membership.CreateUser(item.AdminUsername, item.AdminPassword, item.Email.Address);
			this.logger.Debug("Created admin user.");

			// Creating administrator role
			Roles.CreateRole("Administrator");
			this.logger.Debug("Created administrator role.");

			// Adding user to role
			Roles.AddUserToRole(item.AdminUsername, "Administrator");
			this.logger.Debug("Assigned user to administration role.");

			PostDto defaultPost = new PostDto();

			defaultPost.Title = "Welcome to Dexter!";
			defaultPost.Tags = new[] { "Dexter" };
			defaultPost.Categories = new[] { "Various" };
			defaultPost.Status = ItemStatus.Published;
			defaultPost.PublishAt = DateTimeOffset.Now;
			defaultPost.Author = item.AdminUsername;
			defaultPost.AllowComments = true;
			defaultPost.Content = File.ReadAllText(defaultPostPath).Replace("[SiteDomain]", item.SiteDomain.Host);

			this.postDataService.SaveOrUpdate(defaultPost);
			this.logger.Debug("Created default post.");
		}

		#endregion
	}
}