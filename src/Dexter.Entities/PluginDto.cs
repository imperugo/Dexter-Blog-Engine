#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PluginDto.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/07
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Entities
{
	using System;
	using System.Collections.Generic;

	public class PluginDto
	{
		#region Public Properties

		public IEnumerable<string> Authors { get; set; }

		public string Description { get; set; }

		public int DownloadCount { get; set; }

		public bool Enabled { get; set; }

		public string Id { get; set; }

		public Uri ImageUri { get; set; }

		public bool IsLatestVersion { get; set; }

		public string PackageId { get; set; }

		public string Title { get; set; }

		public Version Version { get; set; }

		#endregion
	}
}