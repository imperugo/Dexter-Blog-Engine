namespace Dexter.Extensions.DateTime
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