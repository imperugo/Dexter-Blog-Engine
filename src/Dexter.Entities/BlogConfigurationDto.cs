#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BlogConfigurationDto.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Entities
{
	public class BlogConfigurationDto
	{
		#region Public Properties

		public string DefaultDomain { get; set; }

		public int DefaultHttpsPort { get; set; }

		public int DefaultPort { get; set; }

		public bool EnableHttps { get; set; }

		public SeoConfigurationDto SeoConfiguration { get; set; }

		public CommentSettingsDto CommentSettings { get; set; }

		#endregion
	}

	public class CommentSettingsDto
	{
		public int NumberOfDayBeforeCloseComments { get; set; }

		public bool EnablePremoderation { get; set; }
	}

	public class SeoConfigurationDto
	{
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
}