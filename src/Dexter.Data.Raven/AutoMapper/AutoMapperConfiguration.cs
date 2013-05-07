#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AutoMapperConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/27
// Last edit:	2013/03/18
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
	using Dexter.Data.Raven.Helpers;
	using Dexter.Entities;

	public class AutoMapperConfiguration
	{
		#region Public Methods and Operators

		public static void Configure()
		{
			Mapper.CreateMap<ItemDto, Item>()
			      .ForMember(dest => dest.SearchContent, opt => opt.MapFrom(x => x.Content.CleanHtmlText()))
				  .ForMember(dest => dest.CommentsId, opt => opt.Ignore())
				  .ForMember(dest => dest.TrackbacksId, opt => opt.Ignore())
			      .Include<PostDto, Post>()
			      .Include<PageDto, Page>();

			Mapper.CreateMap<Item, ItemDto>()
			      .ForMember(dest => dest.Id, opt => opt.MapFrom(x => RavenIdHelper.Resolve(x.Id)))
			      .Include<Post, PostDto>()
			      .Include<Page, PageDto>();

			Mapper.CreateMap<Post, PostDto>();

			Mapper.CreateMap<Page, PageDto>()
			      .ForMember(dest => dest.PagesId, opt => opt.MapFrom(x => RavenIdHelper.Resolve(x.PagesId)))
			      .ForMember(dest => dest.ParentId, opt => opt.MapFrom(x => RavenIdHelper.Resolve(x.ParentId)));

			Mapper.CreateMap<PageDto, Page>()
					.ForMember(dest => dest.Id, opt => opt.MapFrom(x => RavenIdHelper.Resolve<Page>(x.Id)))
					.ForMember(dest => dest.CommentsId, opt => opt.Ignore())
					.ForMember(dest => dest.TrackbacksId, opt => opt.Ignore())
					.ForMember(dest => dest.ParentId, opt => opt.MapFrom(x => RavenIdHelper.Resolve<Page>(x.ParentId)))
					.ForMember(dest => dest.PagesId, opt => opt.MapFrom(x => RavenIdHelper.Resolve<Page>(x.PagesId)));

			Mapper.CreateMap<PostDto, Post>()
				  .ForMember(dest => dest.Results, opt => opt.Ignore())
			      .ForMember(dest => dest.CommentsId, opt => opt.Ignore())
			      .ForMember(dest => dest.TrackbacksId, opt => opt.Ignore())
			      .ForMember(dest => dest.Id, opt => opt.MapFrom(x => RavenIdHelper.Resolve<Post>(x.Id)));

			Mapper.CreateMap<Comment, CommentDto>()
			      .ForMember(dest => dest.ItemInfo, opt => opt.Ignore())
			      .ReverseMap();

			Mapper.CreateMap<CommentDto, Comment>();

			Mapper.CreateMap<Category, CategoryDto>()
			      .ForMember(dest => dest.Id, opt => opt.MapFrom(x => RavenIdHelper.Resolve(x.Id)))
			      .ForMember(dest => dest.PostCount, opt => opt.MapFrom(p => p.PostsId.Length));

			Mapper.CreateMap<CategoryDto, Category>()
			      .ForMember(dest => dest.Id, opt => opt.MapFrom(x => RavenIdHelper.Resolve<Category>(x.Id)))
			      .ForMember(dest => dest.ParentId, opt => opt.MapFrom(x => RavenIdHelper.Resolve<Category>(x.Parent.Id)));
			
			Mapper.CreateMap<EmailMessage, EmailMessageDto>().ReverseMap();
		}

		#endregion
	}
}