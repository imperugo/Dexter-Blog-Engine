#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services.Implmentation
{
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;

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
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
		}

		#endregion
	}
}