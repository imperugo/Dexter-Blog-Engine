#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			EmailMessageDto.cs
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

namespace Dexter.Data.Raven.Domain
{
	using System.Net.Mail;

	public class EmailMessage : EntityBase<int>
	{
		#region Constructors and Destructors

		protected EmailMessage()
		{
		}

		#endregion

		#region Public Properties

		public virtual MailAddress Bcc { get; set; }

		public virtual string Body { get; set; }

		public virtual MailAddress Cc { get; set; }

		public virtual MailAddress MailTo { get; set; }

		public virtual int RetryCount { get; set; }

		public virtual MailAddress Sender { get; set; }

		public virtual string Status { get; set; }

		public virtual string Subject { get; set; }

		#endregion
	}
}