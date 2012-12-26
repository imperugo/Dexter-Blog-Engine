#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IAdminUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/12/26
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Contracts
{
	using Dexter.Entities;
	using Dexter.Navigation.Helpers;

	public enum FeedbackType
	{
		Positive, 

		Negative, 

		Warning
	}

	public interface IAdminUrlBuilder
	{
		#region Public Methods and Operators

		SiteUrl DeletePage(ItemDto item);

		SiteUrl DeletePost(ItemDto item);

		SiteUrl EditPage(ItemDto item);

		SiteUrl EditPost(ItemDto item);

		SiteUrl FeedbackPage(FeedbackType feedback, string localizationKey, SiteUrl redirect);

		SiteUrl Home();

		SiteUrl Post();

		SiteUrl Login();

		#endregion
	}
}