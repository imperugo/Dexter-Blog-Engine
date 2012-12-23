#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SetupBinder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/02
// Last edit:	2012/12/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Setup.Models
{
	using System.ComponentModel.DataAnnotations;

	public class SetupBinder
	{
		#region Public Properties

		[Required]
		[Display(Name = "Password")]
		[RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password must contains a number, lovercase, uppercase and one simbol")]
		[StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		public string AdminPassword { get; set; }

		[Compare("AdminPassword")]
		[Display(Name = "Confirm Password")]
		public string AdminPasswordConfirm { get; set; }

		[Required]
		[Display(Name = "Username")]
		public string AdminUsername { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name = "Blog name")]
		public string BlogName { get; set; }

		[Compare("Email")]
		[Display(Name = "Confirm email")]
		public string ConfirmEmail { get; set; }

		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Url]
		[Required]
		[Display(Name = "Site Domain")]
		public string SiteDomain { get; set; }

		#endregion
	}
}