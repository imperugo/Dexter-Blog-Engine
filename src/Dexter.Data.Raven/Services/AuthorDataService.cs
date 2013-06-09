#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AuthorDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/06/09
// Last edit:	2013/06/09
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Services
{
	using Common.Logging;

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Session;
	using Dexter.Shared.Dto;
	using Dexter.Shared.Requests;

	using global::AutoMapper;

	public class AuthorDataService : ServiceBase, IAuthorDataService
	{
		public AuthorDataService(ILog logger, ISessionFactory sessionFactory)
			: base(logger, sessionFactory)
		{
		}

		public AuthorInfoDto SaveOrUpdate(AuthorRequest author)
		{
			AuthorInfo authorInfo = author.MapTo<AuthorInfo>();

			this.Session.Store(authorInfo);

			return authorInfo.MapTo<AuthorInfoDto>();
		}
	}
}