#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterSettings.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/12/01
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

	public class DexterSettings
	{
		#region Public Properties

		public string DateFormatPatter { get; set; }

		public int DefaultPostCategoryId { get; set; }

		public bool EnableXmlRpc { get; set; }

		public DayOfWeek FirstDayOfWeek { get; set; }

		public Uri MainDomain { get; set; }

		public MailAddress OwnerEmail { get; set; }

		public ReadingConfiguration ReadingConfiguration { get; set; }

		public SeoConfiguration SeoConfiguration { get; set; }

		public SmtpConfiguration SmtpConfiguration { get; set; }

		public string SubTitle { get; set; }

		public string TimeFormatPatter { get; set; }

		public string TimeZoneId { get; set; }

		public string Title { get; set; }

		public Tracking Tracking { get; set; }

		public IList<Uri> UpdateServices { get; set; }

		#endregion
	}

	public class Tracking
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets a value indicating whether [enable ping back receive].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [enable ping back receive]; otherwise, <c>false</c>.
		/// </value>
		public bool EnablePingBackReceive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [enable ping back send].
		/// </summary>
		/// <value><c>true</c> if [enable ping back send]; otherwise, <c>false</c>.</value>
		public bool EnablePingBackSend { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [enable referrer tracking].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [enable referrer tracking]; otherwise, <c>false</c>.
		/// </value>
		public bool EnableReferrerTracking { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [enable track back receive].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [enable track back receive]; otherwise, <c>false</c>.
		/// </value>
		public bool EnableTrackBackReceive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [enable track back send].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [enable track back send]; otherwise, <c>false</c>.
		/// </value>
		public bool EnableTrackBackSend { get; set; }

		#endregion
	}
}