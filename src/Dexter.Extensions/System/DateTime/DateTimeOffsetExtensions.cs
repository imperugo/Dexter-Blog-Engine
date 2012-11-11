#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DateTimeOffsetExtensions.cs
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

namespace System
{
	using System;

	public static class DateTimeOffsetExtensions
	{
		#region Public Methods and Operators

		public static DateTimeOffset AtNoon(this DateTimeOffset date)
		{
			return new DateTimeOffset(date.Year, date.Month, date.Day, 12, 0, 0, 0, date.Offset);
		}

		public static DateTimeOffset AtTime(this DateTimeOffset date, DateTimeOffset time)
		{
			return new DateTimeOffset(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond, date.Offset);
		}

		public static DateTimeOffset WithDate(this DateTimeOffset time, DateTimeOffset date)
		{
			return new DateTimeOffset(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond, time.Offset);
		}

		public static DateTimeOffset WithDate(this DateTimeOffset time, DateTime date)
		{
			return new DateTimeOffset(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond, time.Offset);
		}

		#endregion
	}
}