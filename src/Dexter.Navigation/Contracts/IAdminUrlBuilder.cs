#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IAdminUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/12/31
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
		#region Public Properties

		IAdminPageUrlBuilder Page { get; }

		IAdminPostUrlBuilder Post { get; }

		IAdminCategoryUrlBuilder Category { get; }

		#endregion

		#region Public Methods and Operators

		SiteUrl FeedbackPage(FeedbackType feedback, string localizationKey, SiteUrl redirect);

		SiteUrl Home();

		SiteUrl EditConfiguration();

		SiteUrl Login();

		#endregion
	}

	public interface IAdminCategoryUrlBuilder
	{
		SiteUrl New();

		SiteUrl List();
	}

	public interface IAdminPostUrlBuilder
	{
		#region Public Methods and Operators

		SiteUrl Delete(ItemDto item);

		SiteUrl Edit(ItemDto item);

		SiteUrl New();

		SiteUrl List();

		#endregion
	}

	public interface IAdminPageUrlBuilder
	{
		#region Public Methods and Operators

		SiteUrl Delete(ItemDto item);

		SiteUrl Edit(ItemDto item);

		SiteUrl New();

		SiteUrl List();

		#endregion
	}
}