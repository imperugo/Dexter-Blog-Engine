#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AutoMapperConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/27
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.AutoMapper
{
	using System;

	using global::AutoMapper;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities;

	public class AutoMapperConfiguration
	{
		#region Public Methods and Operators

		public static void Configure()
		{
			Mapper.CreateMap<PostDto, Post>()
			      .ForMember(dest => dest.SearchContent, opt => opt.MapFrom(x => x.Content.CleanHtmlText()))
			      .ReverseMap();

			Mapper.CreateMap<Comment, CommentDto>().ReverseMap();
			Mapper.CreateMap<Category, CategoryDto>().ReverseMap();
			Mapper.CreateMap<EmailMessage, EmailMessageDto>().ReverseMap();
		}

		#endregion
	}
}