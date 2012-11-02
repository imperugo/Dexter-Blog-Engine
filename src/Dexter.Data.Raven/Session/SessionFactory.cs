#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SessionFactory.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/11/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Session
{
	using System;

	using Common.Logging;

	using Dexter.Async;

	using global::Raven.Client;

	using global::Raven.Client.Document;

	public class SessionFactory : ISessionFactory
	{
		#region Constants

		internal const String SESSION_STATE_KEY = "{740123ac-1ef2-422f-b8d9-019bf3f36e1e}";

		#endregion

		#region Fields

		private readonly ICallContextFactory callContextFactory;

		private readonly DocumentStore documentStore;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		public SessionFactory(ICallContextFactory callContextFactory, ILog logger, DocumentStore documentStore)
		{
			this.callContextFactory = callContextFactory;
			this.logger = logger;
			this.documentStore = documentStore;
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="SessionFactory"/> is reclaimed by garbage collection.
		/// </summary>
		~SessionFactory()
		{
			this.EndSession(false);
		}

		#endregion

		#region Public Properties

		public IDocumentSession Session
		{
			get
			{
				ICallContext currentContext = callContextFactory.RetrieveCallContext();

				IDocumentSession session;

				if (currentContext[SESSION_STATE_KEY] == null)
				{
					logger.Debug("Session·Created");
					session = CreateAndPutSessionInContext(currentContext);
				}
				else
				{
					logger.Debug("Session·was·already·created");
					session = (IDocumentSession)currentContext[SESSION_STATE_KEY];
				}

				return session;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void EndSession(bool succesfully)
		{
			ICallContext currentContext = this.callContextFactory.RetrieveCallContext();

			if (currentContext == null)
			{
				return;
			}

			if (currentContext[SESSION_STATE_KEY] == null)
			{
				return;
			}

			IDocumentSession session = currentContext[SESSION_STATE_KEY] as IDocumentSession;

			if (session == null)
			{
				return;
			}

			session.SaveChanges();
		}

		public void StartSession()
		{
		}

		#endregion

		#region Methods

		internal IDocumentSession CreateAndPutSessionInContext(ICallContext dexterContext)
		{
			IDocumentSession session = this.documentStore.OpenSession();

			dexterContext[SESSION_STATE_KEY] = session;
			return session;
		}

		#endregion
	}
}