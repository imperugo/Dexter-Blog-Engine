#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ServiceBase.cs
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

namespace Dexter.Data.Raven.Services
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
			if (this.aggressivelyCacheFor != null)
			{
				this.aggressivelyCacheFor.Dispose();
				this.aggressivelyCacheFor = null;
			}
		}

		#endregion
	}
}