#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPageUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/24
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Navigation.Contracts
{
	using Dexter.Entities;
	using Dexter.Navigation.Helpers;

	public interface IPageUrlBuilder
	{
		#region Public Methods and Operators

		SiteUrl Delete(ItemDto item);

		SiteUrl Edit(ItemDto item);

		SiteUrl Create();

		SiteUrl Permalink(ItemDto item);

		#endregion
	}
}