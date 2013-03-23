#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/28
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Navigation.Contracts
{
	using System.Web;

	using Dexter.Navigation.Helpers;

	public interface IUrlBuilder
	{
		#region Public Properties

		IAdminUrlBuilder Admin { get; }

		SiteUrl Home { get; }

		IPageUrlBuilder Page { get; }

		IPostUrlBuilder Post { get; }

		ICategoryUrlBuilder Category { get; }

		#endregion

		#region Public Methods and Operators

		SiteUrl CurrentUrl(HttpContextBase request);

		SiteUrl PingbackUrl();

		string ResolveUrl(string value);

		#endregion
	}
}