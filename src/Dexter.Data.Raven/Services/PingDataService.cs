#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PingDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/31
// Last edit:	2012/12/31
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Common.Logging;

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Session;

	public class PingDataService : ServiceBase, IPingDataService
	{
		#region Constructors and Destructors

		public PingDataService(ILog logger, ISessionFactory sessionFactory)
			: base(logger, sessionFactory)
		{
		}

		#endregion

		#region Public Methods and Operators

		public void Add(Uri site)
		{
			PingSites pingSites = this.Session.Query<PingSites>().Take(200).FirstOrDefault() ?? new PingSites();

			if (pingSites.Sites.Any(x => x.AbsoluteUri != site.AbsoluteUri))
			{
				pingSites.Sites.Add(site);
				this.Session.Store(pingSites);
			}
		}

		public void Delete(Uri site)
		{
			PingSites pingSites = this.Session.Query<PingSites>().Take(200).FirstOrDefault() ?? new PingSites();

			if (pingSites.Sites.Any(x => x.AbsoluteUri == site.AbsoluteUri))
			{
				pingSites.Sites.Remove(site);
				this.Session.Store(pingSites);
			}
		}

		public IEnumerable<Uri> GetPingSites()
		{
			PingSites pingSites = this.Session.Query<PingSites>().Take(200).FirstOrDefault() ?? new PingSites();

			return pingSites.Sites;
		}

		#endregion
	}
}