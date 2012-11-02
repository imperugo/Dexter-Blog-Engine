#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven
{
	using Dexter.Data.Raven.Services;
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;

	using global::Raven.Client;

	using global::Raven.Client.Embedded;

	public class Installer : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
			
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<IPostDataService, PostDataService>(LifeCycle.Singleton);
			container.Register<IPageDataService, PageDataService>(LifeCycle.Singleton);
			container.Register<ICommentDataService, CommentDataService>(LifeCycle.Singleton);
			container.Register<IBlogConfigurationDataService, BlogConfigurationDataService>(LifeCycle.Singleton);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
			IDocumentStore store = new EmbeddableDocumentStore
				                       {
					                       RunInMemory = false, 
					                       DataDirectory = "App_Data/db", 
				                       };

			store.Initialize();
		}

		#endregion
	}
}