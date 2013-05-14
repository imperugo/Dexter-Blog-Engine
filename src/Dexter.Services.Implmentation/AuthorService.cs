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
	using System;

	using Dexter.Data;
	using Dexter.Shared.Dto;
	using Dexter.Shared.Requests;
	using Dexter.Shared.Validation;

	public class AuthorService : IAuthorService
	{
		private readonly IObjectValidator objectValidator;

		private readonly IAuthorDataService authorDataService;

		public AuthorService(IObjectValidator objectValidator, IAuthorDataService authorDataService)
		{
			this.objectValidator = objectValidator;
			this.authorDataService = authorDataService;
		}

		public AuthorInfoDto SaveOrUpdate(AuthorRequest author)
		{
			this.objectValidator.Validate(author);

			return this.authorDataService.SaveOrUpdate(author);
		}
	}
}