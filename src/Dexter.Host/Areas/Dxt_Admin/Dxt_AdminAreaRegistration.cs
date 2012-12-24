#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Dxt_AdminAreaRegistration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host.Areas.Dxt_Admin
{
	using System.Web.Mvc;

	public class Dxt_AdminAreaRegistration : AreaRegistration
	{
		#region Public Properties

		public override string AreaName
		{
			get
			{
				return "Dxt_Admin";
			}
		}

		#endregion

		#region Public Methods and Operators

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Dxt_Admin_default",
				"Dxt-Admin/{controller}/{action}/{id}",
				new
					{
						action = "Index",
						id = UrlParameter.Optional
					},
				new[] { "Dexter.Host.Areas.Dxt_Admin.Controllers" });
		}

		#endregion
	}
}