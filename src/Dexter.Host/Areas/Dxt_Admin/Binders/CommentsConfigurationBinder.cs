#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CommentsConfigurationBinder.cs
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
	public class CommentsConfigurationBinder
	{
		#region Public Properties

		public bool EnablePremoderation { get; set; }

		public int? NumberOfDayBeforeCloseComments { get; set; }

		#endregion
	}
}