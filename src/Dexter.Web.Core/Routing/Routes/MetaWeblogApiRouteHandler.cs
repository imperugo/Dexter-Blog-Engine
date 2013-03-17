#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			MetaWeblogApiRouteHandler.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/17
// Last edit:	2013/03/17
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Routing.Routes
{
	using System.Web;
	using System.Web.Routing;

	using Dexter.Web.Core.HttpHandlers;

	public class MetaWeblogApiRouteHandler : IRouteHandler
	{
		#region Public Methods and Operators

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new MetaWeblogHandler();
		}

		#endregion
	}
}