namespace Dexter.Services
{
	using Dexter.Shared.Dto;
	using Dexter.Shared.Requests;

	public interface IAuthorService
	{
		AuthorInfoDto SaveOrUpdate(AuthorRequest author);
	}
}