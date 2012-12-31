using System;
using System.Web.Mvc;

namespace Dexter.Web.Mvc.Helpers {
	public static class RouteHelper {
		public static bool IsCurrentController(string area, string controllerName, ViewContext viewContext ) {
			bool result = false;
			string normalizedControllerName = controllerName.EndsWith ( "Controller" )
												? controllerName
												: String.Format ( "{0}Controller", controllerName );

			if (viewContext == null) {
				return false;
			}

			var areaName = viewContext.RouteData.DataTokens["area"] as string;

			if (string.Compare ( areaName, area, true ) != 0)
				return false;

			if (viewContext.Controller.GetType ( ).Name.Equals ( normalizedControllerName, StringComparison.InvariantCultureIgnoreCase )) {
				result = true;
			}

			return result;
		}

		public static bool IsCurrentControllerAndAction ( string area, string controllerName, string actionName, ViewContext viewContext ) {
			bool result = false;
			string normalizedControllerName = controllerName.EndsWith ( "Controller" )
												? controllerName
												: String.Format ( "{0}Controller", controllerName );

			if (viewContext == null) {
				return false;
			}

			var areaName = viewContext.RouteData.DataTokens["area"] as string;

			if (string.Compare ( areaName, area, true ) != 0)
				return false;

			if (viewContext.Controller.GetType ( ).Name.Equals ( normalizedControllerName, StringComparison.InvariantCultureIgnoreCase ) &&
				 viewContext.Controller.ValueProvider.GetValue ( "action" ).AttemptedValue.Equals ( actionName, StringComparison.InvariantCultureIgnoreCase )) {
				result = true;
			}

			return result;
		}
	}
}