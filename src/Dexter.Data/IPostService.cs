namespace Dexter.Data
{
	using System;

	using Dexter.Data.DataTransferObjects;
	using Dexter.Data.DataTransferObjects.Result;

	public interface IPostService
	{
		PostDto GetPostDtoByKey(string slug);

		PostDto GetPostDtoById(int id);

		IPagedResult<PostDto> GetLastPost(int pageIndex, int pageSize, PostQueryFilter filter);
	}

	public class PostQueryFilter
	{
		public DateTimeOffset MaxPublishAt { get; set; }
	}
}