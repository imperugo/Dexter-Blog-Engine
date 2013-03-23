#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/12
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Navigation
{
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;
	using Dexter.Navigation.Concretes;
	using Dexter.Navigation.Contracts;

	public class Installer : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
			
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<IUrlBuilder, UrlBuilder>(LifeCycle.Singleton);
			container.Register<IAdminUrlBuilder, AdminUrlBuilder>(LifeCycle.Singleton);
			container.Register<IPostUrlBuilder, PostUrlBuilder>(LifeCycle.Singleton);
			container.Register<IPageUrlBuilder, PageUrlBuilder>(LifeCycle.Singleton);
			container.Register<ICategoryUrlBuilder, CategoryUrlBuilder>(LifeCycle.Singleton);
			container.Register<IAdminPageUrlBuilder, AdminPageUrlBuilder>(LifeCycle.Singleton);
			container.Register<IAdminPostUrlBuilder, AdminPostUrlBuilder>(LifeCycle.Singleton);
			container.Register<IAdminCategoryUrlBuilder, AdminCategoryUrlBuilder>(LifeCycle.Singleton);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
		}

		#endregion
	}
}