#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RavenDbTestBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test
{
	using System;

	using Dexter.Data.Raven.AutoMapper;
	using Dexter.Data.Raven.Domain;

	using global::Raven.Client;
	using global::Raven.Client.Document;
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
				RunInMemory = true,
				Conventions =
				{
					FindTypeTagName = type =>
					{
						if (typeof(Item).IsAssignableFrom(type))
						{
							return "Items";
						}
						return DocumentConvention.DefaultTypeTagName(type);
					}
				}
			};

			this.documentStore.Initialize();

			Dexter.Data.Raven.Setup.Indexes.UpdateDatabaseIndexes(documentStore);

			IndexCreation.CreateIndexes(this.GetType().Assembly, documentStore);

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

		#endregion
	}
}