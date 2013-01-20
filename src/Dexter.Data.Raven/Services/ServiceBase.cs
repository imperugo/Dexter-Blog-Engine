#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ServiceBase.cs
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

namespace Dexter.Data.Raven.Services
{
	using Common.Logging;

	using Dexter.Data.Raven.Session;

	using global::Raven.Client;

	public class ServiceBase
	{
		#region Fields

		private readonly ILog logger;

		private readonly ISessionFactory sessionFactory;

		#endregion

		#region Constructors and Destructors

		public ServiceBase(ILog logger, ISessionFactory sessionFactory)
		{
			this.logger = logger;
			this.sessionFactory = sessionFactory;
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
				return this.sessionFactory.Session;
			}
		}

		public ISessionFactory SessionFactory
		{
			get
			{
				return this.sessionFactory;
			}
		}

		#endregion
	}
}