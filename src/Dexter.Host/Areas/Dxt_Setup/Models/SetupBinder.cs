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
		public string AdminPassword { get; set; }

		[Compare("AdminPassword")]
		public string AdminPasswordConfirm { get; set; }

		[Required]
		public string AdminUsername { get; set; }

		[Required]
		[StringLength(100)]
		public string BlogName { get; set; }

		[Compare("Email")]
		public string ConfirmEmail { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string SiteDomain { get; set; }

		#endregion
	}
}