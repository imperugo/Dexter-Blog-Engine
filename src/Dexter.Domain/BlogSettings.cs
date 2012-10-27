namespace Dexter.Domain
{
	using System;
	using System.Collections.Generic;
	using System.Net.Mail;

	public class BlogSettings
	{
		#region Public Properties

		public string Title { get; set; }

		public string SubTitle { get; set; }

		public Uri MainUrl { get; set; }

		public MailAddress OwnerEmail { get; set; }

		public int DefaultPostCategoryId { get; set; }

		public DayOfWeek FirstDayOfWeek { get; set; }

		public string DateFormatPatter { get; set; }

		public string TimeFormatPatter { get; set; }

		public string TimeZoneId { get; set; }

		public SeoConfiguration SeoConfiguration { get; set; }

		public SmtpConfiguration SmtpConfiguration { get; set; }

		public ReadingConfiguration ReadingConfiguration { get; set; }

		public bool EnableXmlRpc { get; set; }

		public IList<Uri> UpdateServices { get; set; }

		#endregion
	}
}