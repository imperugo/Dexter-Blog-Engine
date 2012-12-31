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

namespace Dexter.Entities
{
	using System.Net.Mail;

	public class EmailMessageDto
	{
		#region Constructors and Destructors

		protected EmailMessageDto()
		{
		}

		#endregion

		#region Public Properties

		public MailAddress Bcc { get; set; }

		public string Body { get; set; }

		public MailAddress Cc { get; set; }

		public int Id { get; set; }

		public MailAddress MailTo { get; set; }

		public MailAddress Sender { get; set; }

		public virtual string Subject { get; set; }

		#endregion

		#region Public Methods and Operators

		public virtual MailMessage ToMailMessage()
		{
			MailMessage msg = new MailMessage(this.Sender, this.MailTo);
			msg.IsBodyHtml = true;

			if (this.Cc != null)
			{
				msg.CC.Add(this.Cc);
			}

			if (this.Bcc != null)
			{
				msg.Bcc.Add(this.Bcc);
			}

			msg.Subject = this.Subject;
			msg.Body = this.Body;

			return msg;
		}

		#endregion
	}
}