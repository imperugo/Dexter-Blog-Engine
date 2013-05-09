namespace Dexter.Shared.Filters
{
	public class ItemOrderFilter
	{
		public ItemOrderFilter()
		{
			this.SortBy = SortBy.PublishAt;
		}

		public SortBy SortBy { get; set; }
		public bool Ascending { get; set; }
		public bool RandomOrder { get; set; }
	}
}