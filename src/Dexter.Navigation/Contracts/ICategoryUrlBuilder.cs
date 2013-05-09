#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ICategoryUrlBuilder.cs
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

namespace Dexter.Navigation.Contracts
{
	using Dexter.Shared.Dto;
	using Dexter.Navigation.Helpers;

	public interface ICategoryUrlBuilder
	{
		#region Public Methods and Operators

		SiteUrl Permalink(CategoryDto item);

		SiteUrl Feed(CategoryDto item);

		SiteUrl Delete(CategoryDto item);

		SiteUrl Edit(CategoryDto item);

		SiteUrl Create();

		#endregion
	}
}