#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			WebApiConfig.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host
{
	using System.Web.Http;

	public static class WebApiConfig
	{
		#region Public Methods and Operators

		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "DefaultApi", 
				routeTemplate: "api/{controller}/{id}", 
				defaults: new
					          {
						          id = RouteParameter.Optional
					          }
				);
		}

		#endregion
	}
}