#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services.Implmentation
{
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;
	using Dexter.Services.Implmentation.Automapper;

	public class Installer : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<IPostService, PostService>(LifeCycle.Singleton);
			container.Register<IPageService, PageService>(LifeCycle.Singleton);
			container.Register<IConfigurationService, ConfigurationService>(LifeCycle.Singleton);
			container.Register<ICommentService, CommentService>(LifeCycle.Singleton);
			container.Register<ISetupService, SetupService>(LifeCycle.Singleton);
			container.Register<ICategoryService, CategoryService>(LifeCycle.Singleton);
			container.Register<IPluginService, PluginService>(LifeCycle.Singleton);
			container.Register<IAuthorService, AuthorService>(LifeCycle.Singleton);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
			AutoMapperConfiguration.Configure();
		}

		#endregion
	}
}