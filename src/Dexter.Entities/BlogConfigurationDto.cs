#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BlogConfigurationDto.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/28
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
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
			this.CommentSettings = new CommentSettingsDto
				                       {
					                       EnablePremoderation = false,
					                       NumberOfDayBeforeCloseComments = 0
				                       };
			this.DebugInfo = new DebugInfo();
			this.SmtpConfiguration = new SmtpConfiguration
				                         {
					                         SmtpHost = "localhost", 
					                         UseSSL = false,
											 Port = 25
				                         };

			this.Tracking = new Tracking
				                {
									EnablePingBackReceive = true,
									EnablePingBackSend = true,
									EnableReferrerTracking = true,
									EnableTrackBackReceive = true,
									EnableTrackBackSend = true
				                };

			this.ReadingConfiguration = new ReadingConfiguration
				                            {
					                            NumberOfPostPerFeed = 10, 
					                            NumberOfPostPerPage = 10, 
					                            ShowAbstractInFeed = false
				                            };
			this.DefaultHttpsPort = 443;
			this.DefaultHttpPort = 80;
		}

		public BlogConfigurationDto()
		{
		}

		#endregion

		#region Public Properties

		public TimeZoneInfo TimeZone { get; set; }

		public CommentSettingsDto CommentSettings { get; set; }

		public DebugInfo DebugInfo { get; set; }

		public string DefaultDomain
		{
			get
			{
				return this.SiteDomain.Host;
			}
		}

		public int DefaultHttpPort { get; set; }

		public int DefaultHttpsPort { get; set; }

		public bool EnableHttps { get; set; }

		public int Id { get; set; }

		public string Name { get; set; }

		public ReadingConfiguration ReadingConfiguration { get; set; }

		public SeoConfigurationDto SeoConfiguration { get; set; }

		public Uri SiteDomain { get; set; }

		public SmtpConfiguration SmtpConfiguration { get; set; }

		public Tracking Tracking { get; set; }

		public string ThemeName { get; set; }

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
			this.DefaultKeyWords = new string[0];
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

	public class SmtpConfiguration
	{
		#region Public Properties

		/// <summary>
		/// 	Gets or sets the password.
		/// </summary>
		/// <value> The password. </value>
		public string Password { get; set; }

		/// <summary>
		/// 	Gets or sets the port.
		/// </summary>
		/// <value> The port. </value>
		public int Port { get; set; }

		/// <summary>
		/// 	Gets or sets the SMTP host.
		/// </summary>
		/// <value> The SMTP host. </value>
		public string SmtpHost { get; set; }

		/// <summary>
		/// 	Gets or sets a value indicating whether [use SSL].
		/// </summary>
		/// <value> <c>true</c> if [use SSL]; otherwise, <c>false</c> . </value>
		public bool UseSSL { get; set; }

		/// <summary>
		/// 	Gets or sets the username.
		/// </summary>
		/// <value> The username. </value>
		public string Username { get; set; }

		#endregion
	}

	public class DebugInfo
	{
		#region Public Properties

		public bool ShowException { get; set; }

		#endregion
	}

	public class ReadingConfiguration
	{
		#region Public Properties

		public int? HomePageItemId { get; set; }

		public int NumberOfPostPerFeed { get; set; }

		public int NumberOfPostPerPage { get; set; }

		public bool ShowAbstractInFeed { get; set; }

		#endregion
	}
}