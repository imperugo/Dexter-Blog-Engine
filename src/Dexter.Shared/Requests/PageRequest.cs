namespace Dexter.Shared.Requests
{
	using Dexter.Services.Requests;

	public class PageRequest : ItemRequest
	{
		public int? ParentId { get; set; }

		public int SortId { get; set; }

		public int[] PagesId { get; set; }

		public string Template { get; set; }
	}
}