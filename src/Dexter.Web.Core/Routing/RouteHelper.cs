#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RouteHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/31
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Web.Mvc.Helpers
{
	using System;
	using System.Web.Mvc;

	public static class RouteHelper
	{
		#region Public Methods and Operators

		public static bool IsCurrentController(string area, string controllerName, ViewContext viewContext)
		{
			bool result = false;
			string normalizedControllerName = controllerName.EndsWith("Controller")
				                                  ? controllerName
				                                  : string.Format("{0}Controller", controllerName);

			if (viewContext == null)
			{
				return false;
			}

			string areaName = viewContext.RouteData.DataTokens["area"] as string;

			if (string.Compare(areaName, area, true) != 0)
			{
				return false;
			}

			if (viewContext.Controller.GetType().Name.Equals(normalizedControllerName, StringComparison.InvariantCultureIgnoreCase))
			{
				result = true;
			}

			return result;
		}

		public static bool IsCurrentControllerAndAction(string area, string controllerName, string actionName, ViewContext viewContext)
		{
			bool result = false;
			string normalizedControllerName = controllerName.EndsWith("Controller")
				                                  ? controllerName
				                                  : string.Format("{0}Controller", controllerName);

			if (viewContext == null)
			{
				return false;
			}

			string areaName = viewContext.RouteData.DataTokens["area"] as string;

			if (string.Compare(areaName, area, true) != 0)
			{
				return false;
			}

			if (viewContext.Controller.GetType().Name.Equals(normalizedControllerName, StringComparison.InvariantCultureIgnoreCase) &&
			    viewContext.Controller.ValueProvider.GetValue("action").AttemptedValue.Equals(actionName, StringComparison.InvariantCultureIgnoreCase))
			{
				result = true;
			}

			return result;
		}

		#endregion
	}
}