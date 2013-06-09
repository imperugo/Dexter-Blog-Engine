#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AuthorService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/10
// Last edit:	2013/05/10
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Implmentation
{
	using Dexter.Data;
	using Dexter.Shared.Dto;
	using Dexter.Shared.Requests;

	public class AuthorService : IAuthorService
	{
		private readonly IAuthorDataService authorDataService;

		public AuthorService(IAuthorDataService authorDataService)
		{
			this.authorDataService = authorDataService;
		}

		public AuthorInfoDto SaveOrUpdate(AuthorRequest author)
		{
			return this.authorDataService.SaveOrUpdate(author);
		}
	}
}