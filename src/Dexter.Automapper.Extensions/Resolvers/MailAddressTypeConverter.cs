#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			MailAddressTypeConverter.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/23
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Automapper.Extensions.Resolvers
{
	using System.Net.Mail;

	using AutoMapper;

	public class MailAddressypeConverter : TypeConverter<string, MailAddress>
	{
		#region Methods

		protected override MailAddress ConvertCore(string source)
		{
			return new MailAddress(source);
		}

		#endregion
	}
}