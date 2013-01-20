#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AdminPageUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/31
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Navigation.Concretes
{
	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class AdminPageUrlBuilder : UrlBuilderBase, IAdminPageUrlBuilder
	{
		#region Fields

		private readonly IPageUrlBuilder pageUrlBuilder;

		#endregion

		#region Constructors and Destructors

		public AdminPageUrlBuilder(IConfigurationService configurationService, IPageUrlBuilder pageUrlBuilder)
			: base(configurationService)
		{
			this.pageUrlBuilder = pageUrlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		public SiteUrl Delete(ItemDto item)
		{
			return this.pageUrlBuilder.Delete(item);
		}

		public SiteUrl Edit(ItemDto item)
		{
			return this.pageUrlBuilder.Edit(item);
		}

		public SiteUrl List()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Page", "Index", null, null);
		}

		public SiteUrl New()
		{
			return this.pageUrlBuilder.Edit(null);
		}

		#endregion
	}
}