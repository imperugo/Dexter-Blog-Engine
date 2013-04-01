#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ReadingViewModel.cs
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

namespace Dexter.Host.Areas.Dxt_Admin.Models.Settings
{
	using Dexter.Host.Areas.Dxt_Admin.Binders;
	using Dexter.Web.Core.Models;

	public class ReadingViewModel : DexterBackofficeModelBase
	{
		#region Public Properties

		public ReadingConfigurationBinder Reading { get; set; }

		#endregion
	}
}