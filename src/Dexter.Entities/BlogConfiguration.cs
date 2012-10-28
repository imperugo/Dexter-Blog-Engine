#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BlogConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Entities
{
	public class BlogConfiguration
	{
		#region Public Properties

		public string DefaultDomain { get; set; }

		public int DefaultHttpsPort { get; set; }

		public int DefaultPort { get; set; }

		public bool EnableHttps { get; set; }

		#endregion
	}
}