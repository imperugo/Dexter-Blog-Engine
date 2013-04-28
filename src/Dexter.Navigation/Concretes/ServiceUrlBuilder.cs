#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Service.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/27
// Last edit:	2013/04/27
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Concretes
{
	using System;

	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class ServiceUrlBuilder : UrlBuilderBase, IServiceUrlBuilder
	{
		#region Constructors and Destructors

		public ServiceUrlBuilder(IConfigurationService configurationService)
			: base(configurationService)
		{
		}

		#endregion

		#region Public Methods and Operators

		public SiteUrl MetaWebLogApi()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, null, "wlw", "metaweblog", null, null);
		}

		public SiteUrl MetaWeblogRsd()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, null, "Syndication", "Rsd", null, null);
		}

		public SiteUrl Pingback()
		{
			throw new NotImplementedException();
		}

		public SiteUrl OpenSearch()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}