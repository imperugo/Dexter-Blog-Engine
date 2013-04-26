#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AutoMapperConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/23
// Last edit:	2013/03/17
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.App_Start.Automapper
{
	using System;

	using AutoMapper;

	using Dexter.Entities;
	using Dexter.Host.Areas.Dxt_Admin.Binders;
	using Dexter.Host.Areas.Dxt_Setup.Models;

	public class AutoMapperConfiguration
	{
		#region Public Methods and Operators

		public static void Configure()
		{
			Mapper.CreateMap<SetupBinder, Setup>();

			Mapper.CreateMap<ItemBinder, ItemDto>()
			      .ForMember(dest => dest.Content, source => source.MapFrom(p => p.FormattedBody))
			      .ForMember(dest => dest.Tags, source => source.MapFrom(p => p.Tags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)))
			      .ForMember(dest => dest.PublishAt, source => source.MapFrom(p => ConvertPusblishAt(p.PublishAt, p.PublishHour, p.PublishMinutes)))
			      .Include<PostBinder, PostDto>()
			      .Include<PageBinder, PageDto>();

			Mapper.CreateMap<ItemDto, ItemBinder>()
			      .ForMember(dest => dest.PublishHour, source => source.MapFrom(p => p.PublishAt.Hour))
			      .ForMember(dest => dest.PublishMinutes, source => source.MapFrom(p => p.PublishAt.Minute))
			      .ForMember(dest => dest.FormattedBody, source => source.MapFrom(p => p.Content))
			      .ForMember(dest => dest.Tags, source => source.MapFrom(p => string.Join(",", p.Tags)))
				  .Include<PostDto, PostBinder>()
				  .Include<PageDto, PageBinder>();

			Mapper.CreateMap<PostBinder, PostDto>().ReverseMap();
			Mapper.CreateMap<PageBinder, PageDto>().ReverseMap();

			Mapper.CreateMap<CategoryBinder, CategoryDto>().ReverseMap();

			Mapper.CreateMap<BlogConfigurationBinder, BlogConfigurationDto>()
			      .ForMember(dest => dest.TimeZone, source => source.MapFrom(p => TimeZoneInfo.FindSystemTimeZoneById(p.TimeZone)));

			Mapper.CreateMap<BlogConfigurationDto, BlogConfigurationBinder>()
					.ForMember(dest => dest.TimeZone, source => source.MapFrom(p => p.TimeZone.Id));

			Mapper.CreateMap<SeoConfigurationBinder, SeoConfigurationDto>()
				  .ForMember(dest => dest.DefaultKeyWords, source => source.MapFrom(p => CommaSeparedStringToConvertStringArray(p.DefaultKeyWords)));

			Mapper.CreateMap<SeoConfigurationDto, SeoConfigurationBinder>()
			      .ForMember(dest => dest.DefaultKeyWords, source => source.MapFrom(p => ConvertStringArrayToCommaSeparedString(p.DefaultKeyWords)));

			Mapper.CreateMap<TrackingConfigurationBinder, Tracking>().ReverseMap();
			Mapper.CreateMap<CommentsConfigurationBinder, CommentSettingsDto>().ReverseMap();
			Mapper.CreateMap<SmtpConfigurationBinder, SmtpConfiguration>().ReverseMap();
			Mapper.CreateMap<ReadingConfigurationBinder, ReadingConfiguration>().ReverseMap();
		}

		#endregion

		private static string ConvertStringArrayToCommaSeparedString(string[] array)
		{
			if (array == null || array.Length == 0)
			{
				return null;
			}

			return string.Join(",", array);
		}

		private static string[] CommaSeparedStringToConvertStringArray(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}

			return value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
		}
		
		private static DateTimeOffset ConvertPusblishAt(DateTimeOffset source, int hour, int minutes)
		{
			DateTime utc = source.DateTime.ToUniversalTime().Date;
			return new DateTimeOffset(utc.Year, utc.Month, utc.Day, hour, minutes, 0, TimeSpan.Zero);
		}
	}
}