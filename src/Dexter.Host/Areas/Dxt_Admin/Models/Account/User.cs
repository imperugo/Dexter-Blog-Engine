#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			User.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/27
// Last edit:	2013/04/27
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Account
{
	using System;
	using System.Collections.Generic;

	public class User
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Comment { get; set; }
		public bool IsApproved { get; set; }
		public int PasswordFailuresSinceLastSuccess { get; set; }
		public DateTime? LastPasswordFailureDate { get; set; }
		public DateTime? LastActivityDate { get; set; }
		public DateTime? LastLockoutDate { get; set; }
		public DateTime? LastLoginDate { get; set; }
		public DateTime? CreateDate { get; set; }
		public bool IsLockedOut { get; set; }
		public DateTime? LastPasswordChangedDate { get; set; }
		public List<string> Roles { get; set; }
	}
}