#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SmtpConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/DexterBlogEngine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Domain
{
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
}