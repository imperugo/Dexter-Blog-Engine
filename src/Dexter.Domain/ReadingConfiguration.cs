namespace Dexter.Domain
{
	public class ReadingConfiguration
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