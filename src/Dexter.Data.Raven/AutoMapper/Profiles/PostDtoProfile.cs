#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostDtoProfile.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.AutoMapper.Profiles
{
	using global::AutoMapper;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities;

	public class PostDtoProfile : Profile
	{
		#region Methods

		protected override void Configure()
		{
			Mapper.CreateMap<Post, PostDto>();
		}

		#endregion
	}
}