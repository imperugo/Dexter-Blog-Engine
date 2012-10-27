#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DateTimeOffsetUtil.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/DexterBlogEngine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Extensions.DateTime
{
	using System;

	public static class DateTimeOffsetUtil
	{
		#region Public Methods and Operators

		public static DateTimeOffset AsMinutes(this DateTimeOffset self)
		{
			return new DateTimeOffset(self.Year, self.Month, self.Day, self.Hour, self.Minute, 0, 0, self.Offset);
		}

		public static DateTimeOffset ConvertFromJsTimestamp(long timestamp)
		{
			DateTimeOffset origin = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, DateTimeOffset.Now.Offset);
			return origin.AddMilliseconds(timestamp);
		}

		public static DateTimeOffset ConvertFromUnixTimestamp(long timestamp)
		{
			DateTimeOffset origin = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, DateTimeOffset.Now.Offset);
			return origin.AddSeconds(timestamp);
		}

		#endregion
	}
}