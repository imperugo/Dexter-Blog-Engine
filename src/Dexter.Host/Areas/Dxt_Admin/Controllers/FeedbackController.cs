#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			FeedbackController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/29
// Last edit:	2013/01/03
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Controllers
{
	using System;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Localization;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	public class FeedbackController : DexterControllerBase
	{
		#region Fields

		private ILocalizationProvider localizationProvider;

		#endregion

		#region Constructors and Destructors

		public FeedbackController(ILog logger, IConfigurationService configurationService, ILocalizationProvider localizationProvider)
			: base(logger, configurationService)
		{
			this.localizationProvider = localizationProvider;
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Negative(string key, string redirect)
		{
			throw new NotImplementedException();
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Positive(string key, string redirect)
		{
			throw new NotImplementedException();
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Warning(string key, string redirect)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}