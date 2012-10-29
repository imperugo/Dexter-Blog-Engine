﻿#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			NavigationExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace System.Web.Mvc
{
	using Dexter.Navigation.Helpers;

	public static class NavigationExtensions
	{
		#region Public Methods and Operators

		public static ActionResult Redirect(this SiteUrl url)
		{
			return new RedirectResult(url);
		}

		#endregion
	}
}