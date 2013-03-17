#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SessionFactory.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/02
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Session
{
	using Common.Logging;

	using Dexter.Async;

	using global::Raven.Client;

	public class SessionFactory : ISessionFactory
	{
		#region Constants

		internal const string SessionStateKey = "{DEXTER-SESSION-KEY}";

		#endregion

		#region Fields

		private readonly ICallContextFactory callContextFactory;

		private readonly IDocumentStore documentStore;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		public SessionFactory(ICallContextFactory callContextFactory, ILog logger, IDocumentStore documentStore)
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
				ICallContext currentContext = this.callContextFactory.RetrieveCallContext();

				IDocumentSession session;

				if (currentContext[SessionStateKey] == null)
				{
					session = this.documentStore.OpenSession("Dexter");
					currentContext[SessionStateKey] = session;
					this.logger.Debug("Session·Created");
				}
				else
				{
					this.logger.Debug("Session·was·already·created");
					session = (IDocumentSession)currentContext[SessionStateKey];
				}

				return session;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void EndSession(bool succesfully)
		{
			ICallContext currentContext = this.callContextFactory.RetrieveCallContext();

			IDocumentSession session = currentContext[SessionStateKey] as IDocumentSession;

			if (session == null)
			{
				logger.Debug("There is no session into the current context.");
				return;
			}

			try
			{
				if (succesfully)
				{
					logger.Debug("Saving changes");
					session.SaveChanges();
				}
				else
				{
					logger.Debug("An error occured, so I'm not flushing the changes");
				}
			}
			finally
			{
				session.Dispose();
				currentContext[SessionStateKey] = null;
				logger.Debug("Session Removed");
			}
		}

		public void StartSession()
		{
		}

		#endregion
	}
}