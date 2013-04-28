#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IServiceUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/27
// Last edit:	2013/04/27
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Contracts
{
	using Dexter.Navigation.Helpers;

	public interface IServiceUrlBuilder
	{
		#region Public Methods and Operators

		SiteUrl MetaWebLogApi();

		SiteUrl MetaWeblogRsd();

		SiteUrl Pingback();

		SiteUrl OpenSearch();

		#endregion
	}
}