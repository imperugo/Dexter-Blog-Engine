#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SetupRoute.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/23
// Last edit:	2012/12/23
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Routing.Routes
{
	using System;
	using System.Web;
	using System.Web.Routing;

	public class SetupRoute : RouteBase
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	When overridden in a derived class, returns route information about the request.
		/// </summary>
		/// <param name = "httpContext">An object that encapsulates information about the HTTP request.</param>
		/// <returns>
		/// 	An object that contains the values from the route definition if the route matches the current request, or null if the route does not match the request.
		/// </returns>
		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			HttpRequestBase request = httpContext.Request;
			HttpResponseBase response = httpContext.Response;

			string loginRequest = request.QueryString["ReturnUrl"];

			if (loginRequest != null && loginRequest.ToLowerInvariant().Contains("/content/setup"))
			{
				return null;
			}

			response.Redirect("/Dxt_Setup/Home/Index");

			return null;
		}

		/// <summary>
		/// 	When overridden in a derived class, checks whether the route matches the specified values, and if so, generates a URL and retrieves information about the route.
		/// </summary>
		/// <param name = "requestContext">An object that encapsulates information about the requested route.</param>
		/// <param name = "values">An object that contains the parameters for a route.</param>
		/// <returns>
		/// 	An object that contains the generated URL and information about the route, or null if the route does not match <paramref name = "values" />.
		/// </returns>
		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			return null;
		}

		#endregion
	}
}