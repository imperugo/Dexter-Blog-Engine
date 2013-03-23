#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/23
// Last edit:	2013/03/23
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Concretes
{
	using System.Globalization;

	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class CategoryUrlBuilder : UrlBuilderBase, ICategoryUrlBuilder
	{
		#region Constructors and Destructors

		public CategoryUrlBuilder(IConfigurationService configurationService)
			: base(configurationService)
		{
		}

		#endregion

		public SiteUrl Permalink(CategoryDto item)
		{
			string[] segments = item == null ? null : new[]
				                                          {
					                                          item.Slug.ToString(CultureInfo.InvariantCulture)
				                                          };

			return new SiteUrl(this.Domain, this.HttpPort, false, null, "Category", "Archive", segments, null);
		}

		public SiteUrl Feed(CategoryDto item)
		{
			string[] segments = item == null ? null : new[]
				                                          {
					                                          item.Slug.ToString(CultureInfo.InvariantCulture)
				                                          };

			return new SiteUrl(this.Domain, this.HttpPort, false, null, "Feeds", "Category", segments, null);
		}

		public SiteUrl Delete(CategoryDto item)
		{
			string[] segments = item == null ? null : new[]
				                                          {
					                                          item.Id.ToString(CultureInfo.InvariantCulture)
				                                          };

			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Category", "ConfirmDelete", segments, null);
		}

		public SiteUrl Edit(CategoryDto item)
		{
			string[] segments = item == null ? null : new[]
				                                          {
					                                          item.Id.ToString(CultureInfo.InvariantCulture)
				                                          };

			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Category", "Manage", segments, null);
		}

		public SiteUrl Create()
		{
			return this.Edit(null);
		}
	}
}