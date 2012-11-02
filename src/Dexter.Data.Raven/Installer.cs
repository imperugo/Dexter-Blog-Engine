#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven
{
	using System.Linq;

	using Dexter.Async;
	using Dexter.Data.Raven.AutoMapper;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Services;
	using Dexter.Data.Raven.Session;
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;

	using global::Raven.Abstractions.Data;

	using global::Raven.Client;

	using global::Raven.Client.Embedded;

	using global::Raven.Client.Indexes;

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
			container.Register<IConfigurationDataService, ConfigurationDataService>(LifeCycle.Singleton);
			container.Register<IDexterCall, DexterCall>(LifeCycle.Singleton);
			container.Register<ISessionFactory, SessionFactory>(LifeCycle.Singleton);

			IDocumentStore store = new EmbeddableDocumentStore
				                       {
					                       RunInMemory = false, 
					                       DataDirectory = "App_Data/db", 
				                       };

			store.Initialize();

			Setup.Indexes.UpdateDatabaseIndexes(store);

			IndexCreation.CreateIndexes(this.GetType().Assembly, store);

			container.Register(typeof(IDocumentStore), store, LifeCycle.Singleton);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
			AutoMapperConfiguration.Configure();
		}

		#endregion
	}
}