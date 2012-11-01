#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DateTimeTypeConverter.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.AutoMapper.Resolvers
{
	using System;

	using global::AutoMapper;

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