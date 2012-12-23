#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Setup.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/23
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
	using System.Net.Mail;

	public class Setup
	{
		#region Public Properties

		public string AdminPassword { get; set; }

		public string AdminUsername { get; set; }

		public string BlogName { get; set; }

		public MailAddress Email { get; set; }

		public Uri SiteDomain { get; set; }

		#endregion
	}
}