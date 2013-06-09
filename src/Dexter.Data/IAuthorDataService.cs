namespace Dexter.Data
{
	using Dexter.Dependency.Attributes;
	using Dexter.Dependency.Validator;
	using Dexter.Shared.Dto;
	using Dexter.Shared.Requests;

	public interface IAuthorDataService : IValidate
	{
		AuthorInfoDto SaveOrUpdate([Validate] AuthorRequest author);
	}
}