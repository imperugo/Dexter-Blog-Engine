namespace Dexter.Data.Raven.Domain
{
	public class SeoConfiguration
	{
		#region Public Properties

		public bool AllowIndicization { get; set; }

		/// <summary>
		/// 	Gets or sets the copy right.
		/// </summary>
		/// <value> The copy right. </value>
		public string CopyRight { get; set; }

		/// <summary>
		/// 	Gets or sets the default description.
		/// </summary>
		/// <value> The default description. </value>
		public string DefaultDescription { get; set; }

		/// <summary>
		/// 	Gets or sets the default key words.
		/// </summary>
		/// <value> The default key words. </value>
		public string[] DefaultKeyWords { get; set; }

		/// <summary>
		/// 	Gets or sets the default title.
		/// </summary>
		/// <value> The default title. </value>
		public string DefaultTitle { get; set; }

		#endregion
	}
}