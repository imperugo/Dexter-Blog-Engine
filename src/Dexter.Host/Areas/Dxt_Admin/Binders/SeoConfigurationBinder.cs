#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SeoConfigurationBinder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/01
// Last edit:	2013/04/26
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Binders
{
	using System.ComponentModel.DataAnnotations;

	public class SeoConfigurationBinder
	{

		#region Public Properties

		public bool AllowIndicization { get; set; }

		[Required]
		public string CopyRight { get; set; }

		[Required]
		public string DefaultDescription { get; set; }

		[Required]
		public string DefaultKeyWords { get; set; }

		[Required]
		public string DefaultTitle { get; set; }

		#endregion

	}
}