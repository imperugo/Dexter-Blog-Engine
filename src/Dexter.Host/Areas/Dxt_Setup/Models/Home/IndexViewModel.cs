#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IndexViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/02
// Last edit:	2012/12/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Setup.Models.Home
{
	using System;
	using System.Collections.ObjectModel;
	using System.Globalization;

	public class IndexViewModel
	{
		#region Public Properties

		public CultureInfo[] CultureInfos { get; set; }

		public SetupBinder Setup { get; set; }

		public ReadOnlyCollection<TimeZoneInfo> TimesZone { get; set; }

		#endregion
	}
}