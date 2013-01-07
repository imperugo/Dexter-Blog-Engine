#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PackageDto.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2013/01/07
// Last edit:	2013/01/07
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Entities
{
	using System;
	using System.Collections.Generic;

	public class PackageDto
	{
		#region Public Properties

		public IEnumerable<string> Authors { get; set; }

		public string Description { get; set; }

		public int DownloadCount { get; set; }

		public string Id { get; set; }

		public Uri ImageUri { get; set; }

		public bool IsLatestVersion { get; set; }

		public string Title { get; set; }

		public bool IsTheme { get; set; }

		public Version Version { get; set; } 

		#endregion
	}
}