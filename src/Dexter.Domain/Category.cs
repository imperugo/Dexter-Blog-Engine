namespace Dexter.Domain
{
	using System.Collections.Generic;

	public class Category : EntityBase
	{
		public string Name { get; set; }

		public bool IsDefault { get; set; }

		public IList<Category> Categories { get; set; }
	}
}