#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SmtpConfigurationBinder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/01
// Last edit:	2013/04/01
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Binders
{
	using System.ComponentModel.DataAnnotations;

	public class SmtpConfigurationBinder
	{
		#region Public Properties

		public string Password { get; set; }

		[Required]
		public int Port { get; set; }

		[Required]
		public string SmtpHost { get; set; }

		public bool UseSSL { get; set; }

		public string Username { get; set; }

		#endregion
	}
}