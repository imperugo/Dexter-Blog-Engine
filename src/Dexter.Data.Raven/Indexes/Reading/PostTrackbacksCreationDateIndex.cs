namespace Dexter.Data.Raven.Indexes.Reading
{
	using System;
	using System.Linq;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities;

	using global::Raven.Abstractions.Indexing;
	using global::Raven.Client.Indexes;

	public class PostTrackbacksCreationDateIndex : AbstractIndexCreationTask<ItemTrackbacks, PostTrackbacksCreationDateIndex.ReduceResult>
	{
		#region Constructors and Destructors

		public PostTrackbacksCreationDateIndex()
		{
			this.Map = postTrackbacks => postTrackbacks.SelectMany(comment => 
																	comment.Approved, (postComment, comment) 
																		=> new
																			{
																				comment.CreatedAt,
																				CommentId = comment.Id,
																				PostCommentsId = postComment.Id,
																				PostId = postComment.Item.Id,
																				postComment.Item.Status,
																				postComment.Item.ItemPublishedAt
																			});

			this.Store(x => x.CreatedAt, FieldStorage.Yes);
			this.Store(x => x.TrackBackId, FieldStorage.Yes);
			this.Store(x => x.ItemId, FieldStorage.Yes);
			this.Store(x => x.PostTrackbacksId, FieldStorage.Yes);
			this.Store(x => x.Status, FieldStorage.Yes);
			this.Store(x => x.ItemPublishedAt, FieldStorage.Yes);
		}

		#endregion

		public class ReduceResult
		{
			#region Public Properties

			public int TrackBackId { get; set; }

			public DateTimeOffset CreatedAt { get; set; }

			public int PostTrackbacksId { get; set; }

			public int ItemId { get; set; }
			
			public ItemStatus Status { get; set; }

			public DateTimeOffset ItemPublishedAt { get; set; }

			#endregion
		}
	}
}