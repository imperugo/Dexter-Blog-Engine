#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			NoStaleQueriesListener.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test.RavenDb
{
	using global::Raven.Client;

	using global::Raven.Client.Listeners;

	public class NoStaleQueriesListener : IDocumentQueryListener
	{
		#region Public Methods and Operators

		public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
		{
			queryCustomization.WaitForNonStaleResults();
		}

		#endregion
	}
}