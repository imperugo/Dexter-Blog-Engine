#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
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

namespace Dexter.Automapper.Extensions
{
	using System;
	using System.Net.Mail;

	using AutoMapper;

	using Dexter.Automapper.Extensions.Resolvers;
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;

	using UriTypeConverter = Dexter.Automapper.Extensions.Resolvers.UriTypeConverter;

	public class Installer : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
		}

		public void ServiceRegistration(IDexterContainer container)
		{
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
			Mapper.CreateMap<DateTimeOffset, DateTime>().ConvertUsing<DateTimeTypeConverter>();
			Mapper.CreateMap<string, Uri>().ConvertUsing<UriTypeConverter>();
			Mapper.CreateMap<string, MailAddress>().ConvertUsing<MailAddressypeConverter>();
		}

		#endregion
	}
}