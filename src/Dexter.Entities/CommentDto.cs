#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CommentDto.cs
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
	using System.Net.Mail;

	public class CommentDto
	{
		#region Public Properties

		public string Author { get; set; }

		public string Body { get; set; }

		public MailAddress Email { get; set; }

		public ItemBaseInfo ItemInfo { get; set; }

		public Uri WebSite { get; set; }

		#endregion
	}
}