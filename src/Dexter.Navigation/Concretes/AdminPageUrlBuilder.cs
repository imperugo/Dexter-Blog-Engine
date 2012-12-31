using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dexter.Navigation.Concretes
{
	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class AdminPageUrlBuilder : UrlBuilderBase, IAdminPageUrlBuilder
	{
		private readonly IPageUrlBuilder pageUrlBuilder;

		public AdminPageUrlBuilder(IConfigurationService configurationService, IPageUrlBuilder pageUrlBuilder)
			: base(configurationService)
		{
			this.pageUrlBuilder = pageUrlBuilder;
		}

		public SiteUrl Delete(ItemDto item)
		{
			return this.pageUrlBuilder.Delete(item);
		}

		public SiteUrl Edit(ItemDto item)
		{
			return this.pageUrlBuilder.Edit(item);
		}

		public SiteUrl New()
		{
			return this.pageUrlBuilder.Edit(null);
		}

		public SiteUrl List()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Page", "Index", null, null);
		}
	}
}
