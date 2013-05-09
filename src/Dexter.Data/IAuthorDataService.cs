namespace Dexter.Data
{
	using Dexter.Shared.Dto;
	using Dexter.Shared.Requests;

	public interface IAuthorDataService
	{
		AuthorInfoDto SaveOrUpdate(AuthorRequest author);
	}
}