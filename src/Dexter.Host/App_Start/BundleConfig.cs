#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BundleConfig.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host
{
	using System.Web.Optimization;

	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		#region Public Methods and Operators

		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
				"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
				"~/Scripts/jquery.unobtrusive*", 
				"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
				"~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
				"~/Content/themes/base/jquery.ui.core.css", 
				"~/Content/themes/base/jquery.ui.resizable.css", 
				"~/Content/themes/base/jquery.ui.selectable.css", 
				"~/Content/themes/base/jquery.ui.accordion.css", 
				"~/Content/themes/base/jquery.ui.autocomplete.css", 
				"~/Content/themes/base/jquery.ui.button.css", 
				"~/Content/themes/base/jquery.ui.dialog.css", 
				"~/Content/themes/base/jquery.ui.slider.css", 
				"~/Content/themes/base/jquery.ui.tabs.css", 
				"~/Content/themes/base/jquery.ui.datepicker.css", 
				"~/Content/themes/base/jquery.ui.progressbar.css", 
				"~/Content/themes/base/jquery.ui.theme.css"));
		}

		#endregion
	}
}