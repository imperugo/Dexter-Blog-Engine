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