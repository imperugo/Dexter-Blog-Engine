#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SettingsController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/01
// Last edit:	2013/04/01
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Controllers
{
	using System;
	using System.Web.Mvc;

	using AutoMapper;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Host.Areas.Dxt_Admin.Binders;
	using Dexter.Host.Areas.Dxt_Admin.Models.Settings;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Shared;
	using Dexter.Shared.Exceptions;
	using Dexter.Web.Core.Controllers.Web;

	[Authorize(Roles = Constants.AdministratorRole)]
	public class SettingsController : DexterControllerBase
	{
		private readonly IUrlBuilder urlBuilder;

		public SettingsController(ILog logger, IConfigurationService configurationService, IUrlBuilder urlBuilder) : base(logger, configurationService)
		{
			this.urlBuilder = urlBuilder;
		}

		[HttpGet]
		public ActionResult Configuration()
		{
			BlogConfigurationViewModel model = new BlogConfigurationViewModel();
			model.BlogConfiguration = this.BlogConfiguration.MapTo<BlogConfigurationBinder>();

			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Configuration(BlogConfigurationBinder blogConfiguration)
		{
			if (!ModelState.IsValid)
			{
				BlogConfigurationViewModel model = new BlogConfigurationViewModel();
				model.BlogConfiguration = blogConfiguration;
			}

			try
			{
				var configuration = this.BlogConfiguration.MapTo<BlogConfigurationDto>();

				this.ConfigurationService.SaveOrUpdateConfiguration(configuration);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "Configuration Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
			catch (DexterException e)
			{
				this.Logger.ErrorFormat("Unable to save the specified category", e);
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "Configuration Not Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
		}

		[HttpGet]
		public ActionResult Seo()
		{
			SeoViewModel model = new SeoViewModel();
			model.Seo = this.BlogConfiguration.SeoConfiguration.MapTo<SeoConfigurationBinder>();

			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Seo(SeoConfigurationBinder seo)
		{
			if (!ModelState.IsValid)
			{
				SeoViewModel model = new SeoViewModel();
				model.Seo = seo;
			}

			try
			{
				var configuration = this.BlogConfiguration;

				configuration.SeoConfiguration = seo.MapTo<SeoConfigurationDto>();

				this.ConfigurationService.SaveOrUpdateConfiguration(configuration);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "Configuration Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
			catch (DexterException e)
			{
				this.Logger.ErrorFormat("Unable to save the specified category", e);
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "Configuration Not Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
		}

		[HttpGet]
		public ActionResult Tracking()
		{
			TrackingViewModel model = new TrackingViewModel();
			model.Tracking = this.BlogConfiguration.Tracking.MapTo<TrackingConfigurationBinder>();

			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Tracking(TrackingConfigurationBinder tracking)
		{
			if (!ModelState.IsValid)
			{
				TrackingViewModel model = new TrackingViewModel();
				model.Tracking = tracking;
			}

			try
			{
				var configuration = this.BlogConfiguration;

				configuration.Tracking = tracking.MapTo<Tracking>();

				this.ConfigurationService.SaveOrUpdateConfiguration(configuration);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "Configuration Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
			catch (DexterException e)
			{
				this.Logger.ErrorFormat("Unable to save the specified category", e);
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "Configuration Not Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
		}

		[HttpGet]
		public ActionResult Comments()
		{
			CommentsViewModel model = new CommentsViewModel();
			model.Comments = this.BlogConfiguration.CommentSettings.MapTo<CommentsConfigurationBinder>();

			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Comments(CommentsConfigurationBinder comments)
		{
			if (!ModelState.IsValid)
			{
				CommentsViewModel model = new CommentsViewModel();
				model.Comments = comments;
			}

			try
			{
				var configuration = this.BlogConfiguration;

				configuration.CommentSettings = comments.MapTo<CommentSettingsDto>();

				this.ConfigurationService.SaveOrUpdateConfiguration(configuration);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "Configuration Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
			catch (DexterException e)
			{
				this.Logger.ErrorFormat("Unable to save the specified category", e);
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "Configuration Not Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
		}

		[HttpGet]
		public ActionResult Smtp()
		{
			SmtpViewModel model = new SmtpViewModel();
			model.Smtp = this.BlogConfiguration.SmtpConfiguration.MapTo<SmtpConfigurationBinder>();

			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Smtp(SmtpConfigurationBinder smtp)
		{
			if (!ModelState.IsValid)
			{
				SmtpViewModel model = new SmtpViewModel();
				model.Smtp = smtp;
			}

			try
			{
				var configuration = this.BlogConfiguration;

				configuration.SmtpConfiguration = smtp.MapTo<SmtpConfiguration>();

				this.ConfigurationService.SaveOrUpdateConfiguration(configuration);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "Configuration Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
			catch (DexterException e)
			{
				this.Logger.ErrorFormat("Unable to save the specified category", e);
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "Configuration Not Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
		}

		[HttpGet]
		public ActionResult Reading()
		{
			ReadingViewModel model = new ReadingViewModel();
			model.Reading = this.BlogConfiguration.MapTo<ReadingConfigurationBinder>();

			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Reading(ReadingConfigurationBinder reading)
		{
			if (!ModelState.IsValid)
			{
				ReadingViewModel model = new ReadingViewModel();
				model.Reading = reading;
			}

			try
			{
				var configuration = this.BlogConfiguration;

				configuration.ReadingConfiguration = reading.MapTo<ReadingConfiguration>();

				this.ConfigurationService.SaveOrUpdateConfiguration(configuration);

				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Positive, "Configuration Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
			catch (DexterException e)
			{
				this.Logger.ErrorFormat("Unable to save the specified category", e);
				return this.urlBuilder.Admin.FeedbackPage(FeedbackType.Negative, "Configuration Not Saved", this.urlBuilder.Admin.EditSeoConfiguration()).Redirect();
			}
		}
	}
}