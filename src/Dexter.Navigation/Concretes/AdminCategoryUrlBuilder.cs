#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AdminCategoryUrlBuilder.cs
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
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class AdminCategoryUrlBuilder : UrlBuilderBase, IAdminCategoryUrlBuilder
	{
		#region Constructors and Destructors

		public AdminCategoryUrlBuilder(IConfigurationService configurationService)
			: base(configurationService)
		{
		}

		#endregion

		#region Public Methods and Operators

		public SiteUrl List()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Category", "Index", null, null);
		}

		public SiteUrl New()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Category", "Manage", null, null);
		}

		#endregion
	}
}