#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AutoMapperConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/09
// Last edit:	2013/05/10
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Automapper
{
	using AutoMapper;

	using Dexter.Shared.Dto;
	using Dexter.Shared.Requests;

	public static class AutoMapperConfiguration
	{
		public static void Configure()
		{
			Mapper.CreateMap<PostDto, PostRequest>()
			      .ForMember(dest => dest.Author, source => source.MapFrom(p => p.Author.Username));
		}
	}
}