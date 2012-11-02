#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterCall.cs
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

namespace Dexter.Data.Raven
{
	using Dexter.Async;
	using Dexter.Data.Raven.Session;

	public class DexterCall : IDexterCall
	{
		#region Fields

		private readonly ISessionFactory sessionFactory;

		#endregion

		#region Constructors and Destructors

		public DexterCall(ISessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
		}

		#endregion

		#region Public Methods and Operators

		public void Complete(bool succesfully)
		{
			this.sessionFactory.EndSession(succesfully);
		}

		public void StartSession()
		{
			this.sessionFactory.StartSession();
		}

		#endregion
	}
}