#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BlogConfigurationDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/01
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

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Extensions;
	using Dexter.Entities;

	using global::Raven.Client;

	public class BlogConfigurationDataService : ServiceBase, IBlogConfigurationDataService
	{
		#region Constructors and Destructors

		public BlogConfigurationDataService(ILog logger, IDocumentSession session)
			: base(logger, session)
		{
		}

		#endregion

		#region Public Methods and Operators

		public BlogConfiguration GetConfiguration()
		{
			BlogSettings configuration = this.Session.Load<BlogSettings>().First();

			return configuration.MapTo<BlogConfiguration>();
		}

		public void SaveConfiguration(BlogConfiguration configuration)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}