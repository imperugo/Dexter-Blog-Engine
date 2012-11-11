#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RazorViewPage.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/11/11
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.ViewPages
{
	using System.Collections.Generic;
	using System.Web.Mvc;

	public abstract class RazorViewPage : RazorViewPage<dynamic>
	{
		#region Constructors and Destructors

		protected RazorViewPage(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions)
			: base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions)
		{
		}

		protected RazorViewPage(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions, IViewPageActivator viewPageActivator)
			: base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, viewPageActivator)
		{
		}

		#endregion
	}
}