namespace Dexter.Data.Raven.AutoMapper
{
	using System;

	using global::AutoMapper;

	using Dexter.Data.Raven.AutoMapper.Resolvers;

	public class AutoMapperConfiguration
	{
		#region Public Methods and Operators

		public static void Configure()
		{
			Mapper.CreateMap<DateTimeOffset, DateTime>().ConvertUsing<DateTimeTypeConverter>();
		}

		#endregion
	}
}