#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AuthorRequest.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/09
// Last edit:	2013/05/09
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Shared.Requests
{
	using System.ComponentModel.DataAnnotations;
	using System.Net.Mail;

	public class AuthorRequest
	{
		[Range(0, int.MaxValue)]
		public int Id { get; set; }

		[Required]
		public string Username { get; set; }

		public string Description { get; set; }
	}
}