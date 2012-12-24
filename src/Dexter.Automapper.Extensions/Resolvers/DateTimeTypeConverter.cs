#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DateTimeTypeConverter.cs
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
	using System;

	using AutoMapper;

	public class DateTimeTypeConverter : TypeConverter<DateTimeOffset, DateTime>
	{
		#region Methods

		protected override DateTime ConvertCore(DateTimeOffset source)
		{
			return source.DateTime;
		}

		#endregion
	}
}