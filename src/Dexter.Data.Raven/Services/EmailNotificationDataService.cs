#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			EmailNotificationDataService.cs
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

namespace Dexter.Data.Raven.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using global::AutoMapper;

	using Common.Logging;

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Indexes.Updating;
	using Dexter.Data.Raven.Session;
	using Dexter.Shared.Dto;

	using global::Raven.Client;

	public class EmailNotificationDataService : ServiceBase, IEmailNotificationDataService
	{
		#region Fields

		private readonly IDocumentStore store;

		#endregion

		#region Constructors and Destructors

		public EmailNotificationDataService(ILog logger, ISessionFactory sessionFactory, IDocumentStore store)
			: base(logger, sessionFactory)
		{
			this.store = store;
		}

		#endregion

		#region Public Methods and Operators

		public void Dequeue(EmailMessageDto item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", "The email message must be contains a valid instance");
			}

			if (item.Id < 1)
			{
				throw new ArgumentException("The item Id must be greater than 0", "item.Id");
			}

			EmailMessage emailToDelete = this.Session.Load<EmailMessage>(item.Id);

			this.Session.Delete(emailToDelete);
		}

		public IEnumerable<EmailMessageDto> Peek(int size)
		{
			List<EmailMessage> items = this.Session.Query<EmailMessage>().Take(size).ToList();

			items.ForEach(x => UpdateEmailStatus.UpdateIndexes(this.store, this.Session, x));

			return items.MapTo<EmailMessageDto>();
		}

		public void Queue(EmailMessageDto item)
		{
			EmailMessage message;

			if (item.Id > 1)
			{
				message = this.Session.Load<EmailMessage>(item.Id);
				item.MapPropertiesToInstance(message);
				message.RetryCount++;
			}
			else
			{
				message = item.MapTo<EmailMessage>();
			}

			message.Status = null;

			this.Session.Store(message);
		}

		#endregion
	}
}