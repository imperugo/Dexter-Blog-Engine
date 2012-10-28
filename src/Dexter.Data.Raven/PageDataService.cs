#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PageDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/29
// Last edit:	2012/10/29
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven
{
	using System;
	using System.Linq;

	using Common.Logging;

	using Dexter.Data.Exceptions;
	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Extensions;
	using Dexter.Entities;

	using global::Raven.Client;

	public class PageDataService : ServiceBase, IPageDataService
	{
		#region Constructors and Destructors

		public PageDataService(ILog logger, IDocumentSession session)
			: base(logger, session)
		{
		}

		#endregion

		#region Public Methods and Operators

		public PageDto GetPageById(int id)
		{
			if (id < 1)
			{
				throw new ArgumentException("The Id must be greater than 0", "id");
			}

			Page post = this.Session.Query<Page>()
				.First(x => x.Id == id);

			if (post == null)
			{
				throw new ItemNotFoundException("id");
			}

			PageDto result = post.MapTo<PageDto>();

			return result;
		}

		public PageDto GetPageBySlug(string slug)
		{
			if (slug == null)
			{
				throw new ArgumentNullException("slug");
			}

			if (slug == string.Empty)
			{
				throw new ArgumentException("The string must have a value.", "slug");
			}

			Page post = this.Session.Query<Page>()
				.First(x => x.Slug == slug);

			if (post == null)
			{
				throw new ItemNotFoundException("slug");
			}

			PageDto result = post.MapTo<PageDto>();

			return result;
		}

		#endregion
	}
}