#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ReadingConfigurationBinder.cs
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
	public class ReadingConfigurationBinder
	{
		#region Public Properties

		public string EncodingForPageAndFeed { get; set; }

		public int HomePageItemId { get; set; }

		public int NumberOfPostPerFeed { get; set; }

		public int NumberOfPostPerPage { get; set; }

		public bool ShowAbstractInFeed { get; set; }

		#endregion
	}
}