namespace Dexter.Services
{
	using Dexter.Dependency.Attributes;
	using Dexter.Dependency.Castle.Interceptor;
	using Dexter.Dependency.Validator;
	using Dexter.Shared.Dto;
	using Dexter.Shared.Requests;

	public interface IAuthorService : IValidate
	{
		AuthorInfoDto SaveOrUpdate([Validate] AuthorRequest author);
	}
}