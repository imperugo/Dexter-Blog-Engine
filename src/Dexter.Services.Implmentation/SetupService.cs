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

	using Dexter.Data;
	using Dexter.Entities;

	public class SetupService : ISetupService
	{
		#region Fields

		private readonly ICategoryDataService categoryService;

		private readonly IConfigurationDataService configurationDataService;

		private readonly IPostDataService postDataService;

		#endregion

		#region Constructors and Destructors

		public SetupService(IConfigurationDataService configurationDataService, IPostDataService postDataService, ICategoryDataService categoryService)
		{
			this.configurationDataService = configurationDataService;
			this.postDataService = postDataService;
			this.categoryService = categoryService;
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

			Task<string> postContentTask = this.GetDefaultPostContentAsync(defaultPostPath, item.SiteDomain.Host);

			BlogConfigurationDto configuration = new BlogConfigurationDto(item.BlogName, item.SiteDomain);

			this.configurationDataService.SaveConfiguration(configuration);

			//Creating default category
			this.categoryService.SaveOrUpdate("Various", true, null);

			// Creating user
			Membership.CreateUser(item.AdminUsername, item.AdminPassword, item.Email.Address);

			// Creating administrator role
			Roles.CreateRole("Administrator");

			// Adding user to role
			Roles.AddUserToRole(item.AdminUsername, "Administrator");

			PostDto defaultPost = new PostDto();

			defaultPost.Title = "Welcome to Dexter!";
			defaultPost.Tags = new[] { "Dexter" };
			defaultPost.Categories = new[] { "Various" };
			defaultPost.Status = ItemStatus.Published;
			defaultPost.PublishAt = DateTimeOffset.Now.AsMinutes();
			defaultPost.Author = item.AdminUsername;

			defaultPost.Content = await postContentTask;

			this.postDataService.SaveOrUpdate(defaultPost);
		}

		#endregion

		private Task<string> GetDefaultPostContentAsync(string filePath, string host)
		{
			return Task.Run(() => File.ReadAllText(filePath).Replace("[SiteDomain]", host));
		}
	}
}