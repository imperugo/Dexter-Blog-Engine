#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RepositoryFactory.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/02/10
// Last edit:	2013/02/10
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Membership
{
	using BuildingBlocks.Membership.Contract;
	using BuildingBlocks.Membership.RavenDB;
	using BuildingBlocks.Store.RavenDB;

	using global::Raven.Client;

	public class RepositoryFactory : IRepositoryFactory
	{
		private readonly IDocumentStore store;

		public RepositoryFactory(IDocumentStore store)
		{
			this.store = store;
		}

		#region Public Methods and Operators

		public IRoleRepository CreateRoleRepository()
		{
			return new RoleRepositoryImpl(new RavenDbStorage(store));
		}

		public IUserRepository CreateUserRepository()
		{
			return new UserRepositoryImpl(new RavenDbStorage(store));
		}

		#endregion
	}
}