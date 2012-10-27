namespace Dexter.Localization.Po
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;

	[Serializable]
	internal class CultureDictionary
	{
		#region Public Properties

		public CultureInfo Culture { get; set; }

		public IDictionary<string, string> Translations { get; set; }

		#endregion
	}
}