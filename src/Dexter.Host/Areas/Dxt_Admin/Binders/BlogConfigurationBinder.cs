#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BlogConfigurationBinder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/01
// Last edit:	2013/04/01
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Binders
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class BlogConfigurationBinder
	{
		#region Public Properties

		[Required(AllowEmptyStrings = false)]
		public string Name { get; set; }

		[Required(AllowEmptyStrings = false)]
		public Uri SiteDomain { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string TimeZone { get; set; }

		#endregion
	}
}