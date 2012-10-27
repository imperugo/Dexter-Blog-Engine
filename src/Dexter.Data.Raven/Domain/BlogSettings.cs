#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BlogSettings.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Domain
{
	using System;
	using System.Collections.Generic;
	using System.Net.Mail;

	public class BlogSettings
	{
		#region Public Properties

		public string DateFormatPatter { get; set; }

		public int DefaultPostCategoryId { get; set; }

		public bool EnableXmlRpc { get; set; }

		public DayOfWeek FirstDayOfWeek { get; set; }

		public Uri MainUrl { get; set; }

		public MailAddress OwnerEmail { get; set; }

		public ReadingConfiguration ReadingConfiguration { get; set; }

		public SeoConfiguration SeoConfiguration { get; set; }

		public SmtpConfiguration SmtpConfiguration { get; set; }

		public string SubTitle { get; set; }

		public string TimeFormatPatter { get; set; }

		public string TimeZoneId { get; set; }

		public string Title { get; set; }

		public IList<Uri> UpdateServices { get; set; }

		#endregion
	}
}