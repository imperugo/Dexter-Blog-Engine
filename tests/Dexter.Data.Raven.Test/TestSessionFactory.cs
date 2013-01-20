#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TestSessionFactory.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/03
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test
{
	using Dexter.Data.Raven.Session;

	using global::Raven.Client;

	public class TestSessionFactory : ISessionFactory
	{
		#region Fields

		private readonly IDocumentStore store;

		#endregion

		#region Constructors and Destructors

		public TestSessionFactory(IDocumentStore store)
		{
			this.store = store;
			this.Session = this.store.OpenSession();
		}

		#endregion

		#region Public Properties

		public IDocumentSession Session { get; private set; }

		#endregion

		#region Public Methods and Operators

		public void EndSession(bool succesfully)
		{
		}

		public void StartSession()
		{
		}

		#endregion
	}
}