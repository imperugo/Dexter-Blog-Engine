#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Setup.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/23
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services.Requests
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Net.Mail;

	public class SetupRequest
	{
		#region Public Properties

		[Required]
		[RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password must contains a number, lovercase, uppercase and one simbol")]
		[StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		public string AdminPassword { get; set; }

		[Required]
		public string AdminUsername { get; set; }

		[Required]
		[StringLength(100)]
		public string BlogName { get; set; }

		[Required]
		public MailAddress Email { get; set; }

		[Required]
		public Uri SiteDomain { get; set; }

		#endregion
	}
}