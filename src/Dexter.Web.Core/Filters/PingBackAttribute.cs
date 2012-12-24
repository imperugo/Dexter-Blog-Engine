#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PingBackAttribute.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Web.Core.Filters
{
	using System.Web.Mvc;

	using Dexter.Dependency;
	using Dexter.Navigation.Contracts;

	public class PingBackAttribute : ActionFilterAttribute
	{
		#region Public Properties

		public IUrlBuilder UrlBuilder
		{
			get
			{
				return DexterContainer.Resolve<IUrlBuilder>();
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Called by the MVC framework before the action method executes.
		/// </summary>
		/// <param name = "filterContext">The filter context.</param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			filterContext.HttpContext.Response.AppendHeader("x-pingback", this.UrlBuilder.PingbackUrl());
		}

		#endregion
	}
}