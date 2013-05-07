#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterViewEngine.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/11
// Last edit:	2013/05/07
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.ViewEngine
{
	using System.Web.Mvc;

	public class DexterViewEngine : RazorViewEngine
	{
		#region Constructors and Destructors

		public DexterViewEngine(string currentTheme)
		{
			this.ViewLocationFormats = new[]
				                           {
					                           string.Concat("~/Extensions/Themes/", currentTheme, "/Frontend/Views/{1}/{0}.cshtml"),
					                           string.Concat("~/Extensions/Themes/", currentTheme, "Frontend/Views/{1}/{0}.cshtml"),
					                           string.Concat("~/Extensions/Themes/", currentTheme, "Frontend/Views/Shared/{0}.cshtml"),
					                           string.Concat("~/Extensions/Themes/", currentTheme, "Frontend/Views/Shared/{0}.cshtml"),
					                           string.Concat("~/Extensions/Themes/", currentTheme, "Frontend/Views/{1}/{0}.cshtml"),
					                           string.Concat("~/Extensions/Themes/", currentTheme, "Frontend/Views/{1}/{0}.cshtml"),
					                           string.Concat("~/Extensions/Themes/", currentTheme, "Frontend/Views/Shared/{0}.cshtml"),
					                           string.Concat("~/Extensions/Themes/", currentTheme, "Frontend/Views/Shared/{0}.cshtml")
				                           };
		}

		#endregion
	}
}