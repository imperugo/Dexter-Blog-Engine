namespace Dexter.Entities.Filters
{
	using System;

	public class OrderFilter<T>
	{
		public OrderFilter(Func<T, object> orderBy)
		{
			OrderBy = orderBy;
		}

		public Func<T, Object> OrderBy { get; set; }

		public bool Ascending { get; set; }

		public bool RandomOrder { get; set; }
	}
}