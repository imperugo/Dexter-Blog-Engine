#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RavenDbTestBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test
{
	using System;
	using System.Threading;

	using Dexter.Data.Raven.AutoMapper;
	using Dexter.Data.Raven.Indexes.Reading;
	using Dexter.Data.Raven.Setup;
	using Dexter.Data.Raven.Test.RavenDb;

	using global::Raven.Client;

	using global::Raven.Client.Embedded;

	using global::Raven.Client.Indexes;

	public class RavenDbTestBase : IDisposable
	{
		#region Fields

		private readonly EmbeddableDocumentStore documentStore;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Object" /> class.
		/// </summary>
		public RavenDbTestBase()
		{
			this.documentStore = new EmbeddableDocumentStore
				                     {
					                     RunInMemory = true
				                     };

			this.documentStore.Initialize();

			this.documentStore.RegisterListener(new NoStaleQueriesListener());

			Indexes.UpdateDatabaseIndexes(this.documentStore);

			IndexCreation.CreateIndexes(typeof(PostTrackbacksCreationDateIndex).Assembly, this.documentStore);

			AutoMapperConfiguration.Configure();
		}

		#endregion

		#region Public Properties

		public EmbeddableDocumentStore DocumentStore
		{
			get
			{
				return this.documentStore;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void Dispose()
		{
			this.documentStore.Dispose();
		}

		#endregion

		#region Methods

		protected void SetupData(Action<IDocumentSession> action)
		{
			using (IDocumentSession session = this.documentStore.OpenSession())
			{
				action(session);
				session.SaveChanges();
			}
		}

		protected void WaitStaleIndexes()
		{
			while (this.DocumentStore.DocumentDatabase.Statistics.StaleIndexes.Length != 0)
			{
				Thread.Sleep(10);
			}
		}

		#endregion
	}
}