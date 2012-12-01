namespace Dexter.Entities
{
	public class CategoryDto
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public bool IsDefault { get; set; }

		public int PostCount { get; set; }
	}
}