#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BlogConfigurationDto.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/12/23
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Entities
{
	using System;

	public class BlogConfigurationDto
	{
		#region Constructors and Destructors

		public BlogConfigurationDto(string name, Uri siteDomain)
			: this()
		{
			this.Name = name;
			this.SiteDomain = siteDomain;
			this.SeoConfiguration = new SeoConfigurationDto(name);
			this.CommentSettings = new CommentSettingsDto();
		}

		public BlogConfigurationDto()
		{
		}

		#endregion

		#region Public Properties

		public CommentSettingsDto CommentSettings { get; set; }

		public string DefaultDomain { get; set; }

		public int DefaultHttpsPort { get; set; }

		public int DefaultPort { get; set; }

		public bool EnableHttps { get; set; }

		public int Id { get; set; }

		public string Name { get; set; }

		public SeoConfigurationDto SeoConfiguration { get; set; }

		public Uri SiteDomain { get; set; }

		public Tracking Tracking { get; set; }

		#endregion
	}

	public class CommentSettingsDto
	{
		#region Constructors and Destructors

		public CommentSettingsDto()
		{
			this.EnablePremoderation = false;
		}

		#endregion

		#region Public Properties

		public bool EnablePremoderation { get; set; }

		public int? NumberOfDayBeforeCloseComments { get; set; }

		#endregion
	}

	public class SeoConfigurationDto
	{
		#region Constructors and Destructors

		public SeoConfigurationDto(string blogName)
		{
			this.AllowIndicization = true;
			this.DefaultTitle = string.Format("{0} | ", blogName);
		}

		#endregion

		#region Public Properties

		public bool AllowIndicization { get; set; }

		/// <summary>
		/// 	Gets or sets the copy right.
		/// </summary>
		/// <value> The copy right. </value>
		public string CopyRight { get; set; }

		/// <summary>
		/// 	Gets or sets the default description.
		/// </summary>
		/// <value> The default description. </value>
		public string DefaultDescription { get; set; }

		/// <summary>
		/// 	Gets or sets the default key words.
		/// </summary>
		/// <value> The default key words. </value>
		public string[] DefaultKeyWords { get; set; }

		/// <summary>
		/// 	Gets or sets the default title.
		/// </summary>
		/// <value> The default title. </value>
		public string DefaultTitle { get; set; }

		#endregion
	}

	public class Tracking
	{
		#region Constructors and Destructors

		public Tracking()
		{
			this.EnablePingBackReceive = true;
			this.EnablePingBackSend = true;
			this.EnableReferrerTracking = true;
			this.EnableTrackBackReceive = true;
			this.EnableTrackBackSend = true;
		}

		#endregion

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