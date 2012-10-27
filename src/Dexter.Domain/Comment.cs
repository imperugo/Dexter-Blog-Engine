namespace Dexter.Domain
{
	using System;
	using System.Net.Mail;

	public enum CommentStatus
	{
		IsSpam,
		IsDeleted,
		IsApproved,
		Pending
	}

	public class Comment : EntityBase
	{
		public string Body { get; set; }
		public string Author { get; set; }

		public virtual MailAddress Email { get; set; }
		public virtual Uri WebSite { get; set; }
		public virtual bool Notify { get; set; }

		public bool Important { get; set; }
		public CommentStatus Status { get; set; }

		public string UserHostAddress { get; set; }
		public string UserAgent { get; set; }
	}
}