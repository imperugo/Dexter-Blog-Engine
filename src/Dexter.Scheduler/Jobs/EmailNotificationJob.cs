#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			EmailNotificationJob.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/31
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Scheduler.Jobs
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Net.Mail;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Shared.Dto;

	using global::Quartz;

	public class EmailNotificationJob : IJob
	{
		#region Fields

		private readonly IConfigurationDataService configurationDataService;

		private readonly IEmailNotificationDataService emailNotificationDataService;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		public EmailNotificationJob(ILog logger, IEmailNotificationDataService emailNotificationDataService, IConfigurationDataService configurationDataService)
		{
			this.logger = logger;
			this.emailNotificationDataService = emailNotificationDataService;
			this.configurationDataService = configurationDataService;
		}

		#endregion

		#region Public Methods and Operators

		public void Execute(IJobExecutionContext context)
		{
			this.logger.Debug("EmailNotificationJob executing.");

			IEnumerable<EmailMessageDto> emails = this.emailNotificationDataService.Peek(5);

			BlogConfigurationDto configuration = this.configurationDataService.GetConfiguration();

			foreach (EmailMessageDto email in emails)
			{
				try
				{
					using (SmtpClient smtp = new SmtpClient(configuration.SmtpConfiguration.SmtpHost, configuration.SmtpConfiguration.Port))
					{
						smtp.EnableSsl = configuration.SmtpConfiguration.UseSSL;
						smtp.Credentials = new NetworkCredential(configuration.SmtpConfiguration.Username, configuration.SmtpConfiguration.Password);

						using (MailMessage msg = email.ToMailMessage())
						{
							smtp.Send(msg);
							this.logger.DebugFormat("Email sent! - Subject: {0} - To {1}", email.Subject, email.MailTo);
						}

						this.emailNotificationDataService.Dequeue(email);
					}
				}
				catch (Exception e)
				{
					this.logger.Error("Error occurred during sending the message.", e);
					this.emailNotificationDataService.Queue(email);
				}
			}

			this.logger.Debug("EmailNotificationJob executed.");
		}

		#endregion
	}
}