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
	using System.Threading;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Host.Areas.Dxt_Admin.Models.Feedback;
	using Dexter.Localization;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	public class FeedbackController : DexterControllerBase
	{
		#region Fields

		private readonly ILocalizationProvider localizationProvider;

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
		public ActionResult Negative(string key, string url)
		{
			var model = this.CreateModel(key, url, FeedbackType.Negative);

			return this.View(model);
		}

		private IndexViewModel CreateModel(string key, string url,FeedbackType feedback)
		{
			IndexViewModel model = new IndexViewModel();

			model.FeedbackType = feedback;

			if (feedback == FeedbackType.Positive && !string.IsNullOrWhiteSpace(url))
			{
				model.Message = this.localizationProvider.GetLocalizedString("Core", key, Thread.CurrentThread.CurrentCulture.Name, url);
			}
			else
			{
				model.Message = this.localizationProvider.GetLocalizedString(key);
			}

			if (!string.IsNullOrEmpty(url))
			{
				model.Redirect = new Uri(url);
			}
			return model;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Positive(string key, string url)
		{
			var model = this.CreateModel(key, url, FeedbackType.Positive);

			return this.View(model);
		}

		#endregion
	}
}