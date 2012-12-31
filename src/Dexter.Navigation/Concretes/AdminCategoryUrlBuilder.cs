#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AdminCategoryUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/31
// Last edit:	2012/12/31
// License:		GNU Library General Public License (LGPL)
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

		public SiteUrl New()
		{
			throw new System.NotImplementedException();
		}

		public SiteUrl List()
		{
			throw new System.NotImplementedException();
		}
	}
}