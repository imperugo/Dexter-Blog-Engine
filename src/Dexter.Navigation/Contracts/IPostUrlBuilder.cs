#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPostUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/01
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

	public interface IPostUrlBuilder
	{
		#region Public Methods and Operators

		SiteUrl ArchivePosts(int month, int year);

		SiteUrl Delete(ItemDto item);

		SiteUrl Edit(ItemDto item);

		SiteUrl Permalink(ItemDto item);

		SiteUrl TrackBack(ItemDto item);

		#endregion
	}
}