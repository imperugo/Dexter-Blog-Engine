namespace Dexter.Data.Raven.Indexes.Reading
{
	using System.Linq;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities;

	using global::Raven.Abstractions.Indexing;
	using global::Raven.Client.Indexes;

	public class PageSlugs : AbstractIndexCreationTask<Page, PageSlugs.ReduceResult>
	{
		public PageSlugs()
		{
			this.Map = pages => pages.Where(page => page.Status == ItemStatus.Published)
			                         .Select(page => new
				                                         {
					                                         Slug = page.Slug
				                                         });

			this.Store(x => x.Slug, FieldStorage.Yes);
		}

		public class ReduceResult
		{
			public string Slug { get; set; }
		}
	}
}