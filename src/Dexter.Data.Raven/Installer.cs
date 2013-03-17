#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/03/11
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven
{
	using System.Configuration;
	using System.Diagnostics;
	using System.Net;
	using System.Net.Sockets;
	using System.Web;

	using BuildingBlocks.Membership.Contract;

	using Dexter.Async;
	using Dexter.Data.Raven.AutoMapper;
	using Dexter.Data.Raven.Listeners;
	using Dexter.Data.Raven.Membership;
	using Dexter.Data.Raven.Services;
	using Dexter.Data.Raven.Session;
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;
	using Dexter.Shared.Exceptions;

	using global::Raven.Client;

	using global::Raven.Client.Document;

	using global::Raven.Client.Embedded;
	using global::Raven.Client.Extensions;
	using global::Raven.Client.Indexes;

	using global::Raven.Client.MvcIntegration;

	public class Installer : ILayerInstaller
	{
		#region Static Fields

		private static IDocumentStore store;

		#endregion

		#region Properties

		private bool UseEmbedded
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["RavenDb"] == null;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
			this.InitializeRavenProfiler();
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<IPostDataService, PostDataService>(LifeCycle.Singleton);
			container.Register<IPageDataService, PageDataService>(LifeCycle.Singleton);
			container.Register<ICommentDataService, CommentDataService>(LifeCycle.Singleton);
			container.Register<IConfigurationDataService, ConfigurationDataService>(LifeCycle.Singleton);
			container.Register<ICategoryDataService, CategoryDataService>(LifeCycle.Singleton);
			container.Register<IDexterCall, DexterCall>(LifeCycle.Singleton);
			container.Register<ISessionFactory, SessionFactory>(LifeCycle.Singleton);
			container.Register<IPluginDataService, PluginDataService>(LifeCycle.Singleton);
			container.Register<IRepositoryFactory, RepositoryFactory>(LifeCycle.Singleton);
			this.InitializeDocumentStore();

			container.Register(typeof(IDocumentStore), store, LifeCycle.Singleton);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
			BuildingBlocks.Membership.RepositoryFactory.Initialize(container.Resolve<IRepositoryFactory>());
			AutoMapperConfiguration.Configure();
		}

		#endregion

		#region Methods

		private void InitializeDocumentStore()
		{
			if (store != null)
			{
				return;
			}

			if (this.UseEmbedded)
			{
				store = new EmbeddableDocumentStore
					        {
						        RunInMemory = false, 
						        DataDirectory = "App_Data/db"
					        }.Initialize();

				((EmbeddableDocumentStore)store).RegisterListener(new NoStaleQueriesListener());
			}
			else
			{
				store = new DocumentStore
					        {
								ConnectionStringName = "RavenDB"
					        }.Initialize();
			}

			TryCreatingIndexes(this.UseEmbedded ? "Embedded" : ConfigurationManager.ConnectionStrings["RavenDB"].ConnectionString);
		}

		private void TryCreatingIndexes(string connectionString)
		{
			try
			{
				store.DatabaseCommands.EnsureDatabaseExists("Dexter");

				Setup.Indexes.UpdateDatabaseIndexes(store);

				IndexCreation.CreateIndexes(this.GetType().Assembly, store);
			}
			catch (WebException e)
			{
				var socketException = e.InnerException as SocketException;
				
				if (socketException == null)
				{
					throw;
				}

				switch (socketException.SocketErrorCode)
				{
					case SocketError.AddressNotAvailable:
					case SocketError.NetworkDown:
					case SocketError.NetworkUnreachable:
					case SocketError.ConnectionAborted:
					case SocketError.ConnectionReset:
					case SocketError.TimedOut:
					case SocketError.ConnectionRefused:
					case SocketError.HostDown:
					case SocketError.HostUnreachable:
					case SocketError.HostNotFound:
						throw new DexterDatabaseConnectionException("Unable to connect to RavenDB", socketException, connectionString);
					default:
						throw;
				}
			}
		}

		[Conditional("DEBUG")]
		private void InitializeRavenProfiler()
		{
			RavenProfiler.InitializeFor(store);
		}

		#endregion
	}
}