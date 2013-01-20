#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AutoMapperConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/23
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.App_Start.Automapper
{
	using AutoMapper;

	using Dexter.Entities;
	using Dexter.Host.Areas.Dxt_Setup.Models;

	public class AutoMapperConfiguration
	{
		#region Public Methods and Operators

		public static void Configure()
		{
			Mapper.CreateMap<SetupBinder, Setup>();
		}

		#endregion
	}
}