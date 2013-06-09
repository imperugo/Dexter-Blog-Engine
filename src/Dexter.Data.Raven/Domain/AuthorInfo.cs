namespace Dexter.Data.Raven.Domain
{
	public class AuthorInfo : EntityBase<string>
	{
		public string Username { get; set; }
		public string Description { get; set; }
	}
}