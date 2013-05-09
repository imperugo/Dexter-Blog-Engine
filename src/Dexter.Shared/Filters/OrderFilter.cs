namespace Dexter.Shared.Filters
{
	using System;

	public class OrderFilter<T>
	{
		public OrderFilter(Func<T, object> orderBy)
		{
			this.OrderBy = orderBy;
		}

		public Func<T, Object> OrderBy { get; set; }

		public bool Ascending { get; set; }

		public bool RandomOrder { get; set; }
	}
}