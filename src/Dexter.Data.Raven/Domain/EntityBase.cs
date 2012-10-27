namespace Dexter.Data.Raven.Domain
{
	using System;

	public class EntityBase
	{

		public int Id { get; set; }

		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }
	}
}
