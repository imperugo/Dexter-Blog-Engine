#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AdminPostUrlBuilder.cs
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

	public class AdminPostUrlBuilder : UrlBuilderBase, IAdminPostUrlBuilder
	{
		#region Fields

		private readonly IPostUrlBuilder postUrlBuilder;

		#endregion

		#region Constructors and Destructors

		public AdminPostUrlBuilder(IConfigurationService configurationService, IPostUrlBuilder postUrlBuilder)
			: base(configurationService)
		{
			this.postUrlBuilder = postUrlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		public SiteUrl Delete(ItemDto item)
		{
			return this.postUrlBuilder.Delete(item);
		}

		public SiteUrl Edit(ItemDto item)
		{
			return this.postUrlBuilder.Edit(item);
		}

		public SiteUrl List()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Post", "Index", null, null);
		}

		public SiteUrl New()
		{
			return this.postUrlBuilder.Edit(null);
		}

		#endregion
	}
}