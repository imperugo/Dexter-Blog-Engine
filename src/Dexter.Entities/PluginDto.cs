namespace Dexter.Entities
{
	using System;
	using System.Collections.Generic;

	public class PluginDto
	{
		public IEnumerable<string> Authors { get; set; }

		public string Description { get; set; }

		public int DownloadCount { get; set; }

		public string Id { get; set; }

		public string PackageId { get; set; }

		public Version Version { get; set; }

		public Uri ImageUri { get; set; }

		public bool IsLatestVersion { get; set; }

		public string Title { get; set; }

		public bool Enabled { get; set; }
	}
}