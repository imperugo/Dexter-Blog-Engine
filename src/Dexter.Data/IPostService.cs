namespace Dexter.Data
{
	using Dexter.Data.DataTransferObjects;

	public interface IPostService
	{
		PostDto GetPostDtoByKey(string slug);

		PostDto GetPostDtoById(int id);
	}
}