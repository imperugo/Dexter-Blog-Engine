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
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web.Security;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Entities;
	using Dexter.Shared;

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

		public async Task InitializeAsync(Setup item)
		{
			string defaultPostPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Setup/defaultPost.dxt");

			var defaultPostTask = this.GetDefaultPostContent(defaultPostPath, item.SiteDomain.Host);
			var membershipTask = this.CreateMembershipAndRole(item);

			BlogConfigurationDto configuration = new BlogConfigurationDto(item.BlogName, item.SiteDomain);
			configuration.TimeZone = TimeZoneInfo.FindSystemTimeZoneById("UTC");

			this.configurationDataService.CreateSetupConfiguration(configuration);
			this.logger.Debug("Created blog configuration.");

			//Creating default category
			this.categoryService.SaveOrUpdate("Various", true, null);
			this.logger.Debug("Created default category.");

			PostDto defaultPost = new PostDto();

			defaultPost.Title = "Welcome to Dexter!";
			defaultPost.Tags = new[] { "Dexter" };
			defaultPost.Categories = new[] { "Various" };
			defaultPost.Status = ItemStatus.Published;
			defaultPost.PublishAt = DateTimeOffset.Now.AsMinutes();
			defaultPost.Author = item.AdminUsername;
			defaultPost.AllowComments = true;

			await Task.WhenAll(defaultPostTask, membershipTask);

			defaultPost.Content = defaultPostTask.Result;

			this.postDataService.SaveOrUpdate(defaultPost);
			this.logger.Debug("Created default post.");
		}

		#endregion

		public Task CreateMembershipAndRole(Setup item)
		{
			//NOTE:The membership use a different session, so it could be runned in an async thread. Unluckily this request is not under transaction

			return Task.Run(() =>
				{
					var user = Membership.GetUser(item.AdminUsername);

					if (user == null)
					{
						// Creating user
						Membership.CreateUser(item.AdminUsername, item.AdminPassword, item.Email.Address);
						this.logger.Debug("Created admin user.");
					}

					var roles = Roles.GetAllRoles();

					if (!roles.Contains(Constants.AdministratorRole))
					{
						// Creating administrator role
						Roles.CreateRole(Constants.AdministratorRole);
						this.logger.Debug("Created administrator role.");
					}

					if (!roles.Contains(Constants.Editor))
					{
						// Creating administrator role
						Roles.CreateRole(Constants.Editor);
						this.logger.Debug("Created editor role.");
					}

					if (!roles.Contains(Constants.Moderator))
					{
						// Creating administrator role
						Roles.CreateRole(Constants.Moderator);
						this.logger.Debug("Created moderator role.");
					}

					// Adding user to role
					Roles.AddUserToRole(item.AdminUsername, Constants.AdministratorRole);
					this.logger.Debug("Assigned user to administration role.");
				});
		}

		public Task<string> GetDefaultPostContent(string defaultPostPath, string host)
		{
			return Task.Run(() => File.ReadAllText(defaultPostPath).Replace("[SiteDomain]", host));
		}
	}
}