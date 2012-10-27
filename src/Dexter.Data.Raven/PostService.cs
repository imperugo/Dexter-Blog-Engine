namespace Dexter.Data.Raven
{
	using System.Linq;

	using Common.Logging;

	using Dexter.Data.DataTransferObjects;
	using Dexter.Data.Raven.Extensions;
	using Dexter.Domain;

	using global::Raven.Client;
	using global::Raven.Client.Linq;

	public class PostService : ServiceBase, IPostService
	{
		public PostService(ILog logger, IDocumentSession session)
			: base(logger, session)
		{
		}

		public PostDto GetPostDtoByKey(string slug)
		{
			var post = this.Session
					.Query<Post>()
					.Where(x => x.Slug == slug)
					.Include(x => x.CommentsId)
					.Include(x => x.CategoriesId)
					.First();

			var result = post.MapTo<PostDto>();

			return result;
		}

		public PostDto GetPostDtoById(int id)
		{
			var post = this.Session
					.Query<Post>()
					.Where(x => x.Id == id)
					.Include(x => x.CommentsId)
					.Include(x => x.CategoriesId)
					.First();

			var result = post.MapTo<PostDto>();

			return result;
		}
	}
}