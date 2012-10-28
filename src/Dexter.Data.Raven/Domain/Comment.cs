#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Comment.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Domain
{
	using System;
	using System.Net.Mail;

	public class Comment : EntityBase
	{
		#region Public Properties

		public string Author { get; set; }

		public string Body { get; set; }

		public virtual MailAddress Email { get; set; }

		public virtual bool Notify { get; set; }

		public string UserAgent { get; set; }

		public string UserHostAddress { get; set; }

		public virtual Uri WebSite { get; set; }

		#endregion
	}
}