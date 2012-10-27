namespace Dexter.Data.Raven
{
	using System;

	using Common.Logging;

	using global::Raven.Client;

	public class ServiceBase : IDisposable
	{
		#region Fields

		private readonly ILog logger;

		private readonly IDocumentSession session;

		private IDisposable aggressivelyCacheFor;

		#endregion

		#region Constructors and Destructors

		public ServiceBase(ILog logger, IDocumentSession session)
		{
			this.logger = logger;
			this.session = session;
			this.aggressivelyCacheFor = session.Advanced.DocumentStore.AggressivelyCacheFor(TimeSpan.FromMinutes(10));
		}

		#endregion

		#region Public Properties

		public ILog Logger
		{
			get
			{
				return this.logger;
			}
		}

		public IDocumentSession Session
		{
			get
			{
				return this.session;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void Dispose()
		{
			if (aggressivelyCacheFor != null)
			{
				aggressivelyCacheFor.Dispose();
				aggressivelyCacheFor = null;
			}
		}

		#endregion
	}
}