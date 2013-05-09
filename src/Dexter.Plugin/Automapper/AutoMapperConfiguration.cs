#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AutoMapperConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/07
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.PackageInstaller.Automapper
{
	using AutoMapper;

	using Dexter.Shared.Dto;
	using Dexter.PackageInstaller.Extensions;

	using NuGet;

	public class AutoMapperConfiguration
	{
		#region Public Methods and Operators

		public static void Configure()
		{
			Mapper.CreateMap<IPackage, PackageDto>()
			      .ForMember(dest => dest.ImageUri, source => source.MapFrom(x => x.IconUrl))
			      .ForMember(dest => dest.IsTheme, source => source.MapFrom(x => x.IsTheme()));
		}

		#endregion
	}
}