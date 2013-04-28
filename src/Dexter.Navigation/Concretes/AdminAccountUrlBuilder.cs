using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dexter.Navigation.Concretes
{
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;
	using Dexter.Services;

	public class AdminAccountUrlBuilder : UrlBuilderBase, IAdminAccountUrlBuilder
	{
		public AdminAccountUrlBuilder(IConfigurationService configurationService)
			: base(configurationService)
		{
		}

		public SiteUrl List()
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Account", "Index", null, null);
		}

		public SiteUrl Delete(string username)
		{
			return new SiteUrl(this.Domain, this.HttpPort, false, "Dxt-Admin", "Account", "ConfirmDelete", new[]
				                                                                                       {
					                                                                                       username
				                                                                                       }, null);
		}
	}
}
