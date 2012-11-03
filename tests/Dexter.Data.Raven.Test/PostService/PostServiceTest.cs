#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostServiceTest.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test.PostService
{
	using System;
	using System.Collections.Generic;

	using Common.Logging;

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Extensions;
	using Dexter.Data.Raven.Services;
	using Dexter.Data.Raven.Test.PostService.Helpers;
	using Dexter.Entities;

	using FizzWare.NBuilder;

	using Moq;

	using SharpTestsEx;

	using Xunit;

	public class PostServiceTest : RavenDbTestBase, IDisposable
	{
		#region Fields

		private readonly PostDataService sut;

		private readonly TestSessionFactory sessionFactory;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Object" /> class.
		/// </summary>
		public PostServiceTest()
		{
			this.sessionFactory = new TestSessionFactory(this.DocumentStore);
			this.sut = new PostDataService(new Mock<ILog>().Object, sessionFactory, this.DocumentStore);
		}

		#endregion

		#region Public Methods and Operators

		public new void Dispose()
		{
			this.sut.Session.Dispose();
		}

		[Fact]
		public void GetPostById_WithValidData_ShouldReturnTheCorrectItem()
		{
			// create repository
			IList<Post> posts = PostHelper.GetPosts(5);

			foreach (Post post in posts)
			{
				this.sut.Session.Store(post);
			}

			this.sut.Session.SaveChanges();

			PostDto expectedPost = this.sut.GetPostByKey(posts[2].Id);

			expectedPost.Should().Not.Be.Null();
			expectedPost.Title.Should().Be.EqualTo(posts[2].Title);
		}

		[Fact]
		public void SavePost_WithValidData_ShouldSaveThePostAndTheItemComments()
		{
			var post = PostHelper.GetPostsDto(1)[0];

			this.sut.SaveOrUpdate(post);

			this.sut.Session.SaveChanges();

			post.Id.Should().Be.GreaterThan(0);

			var postEntity = this.sut.Session
										.Include<Post>(x => x.CommentsId)
										.Load<Post>(post.Id);

			postEntity.Should().Not.Be.Null();
			postEntity.Title.Should().Be.EqualTo(post.Title);
			post.Slug.Should().Not.Be.Null();

			var itemComments = this.sut.Session.Load<ItemComments>(postEntity.CommentsId);

			itemComments.Should().Not.Be.Null();
		}

		[Fact]
		public void SavePost_ChangingSlug_ShouldUpdateDenormalizedData()
		{
			Post post;
			ItemComments comments;

			using(var testSession = this.DocumentStore.OpenSession())
			{
				post = PostHelper.GetPosts(1)[0];
				post.Status = ItemStatus.Published;
				post.PublishAt = DateTime.Today;

				testSession.Store(post);

				comments = new ItemComments();
				comments.Approved = new List<Comment>();
				comments.Item = new ItemReference
				{
					Id = post.Id,
					Status = post.Status,
					ItemPublishedAt = post.PublishAt
				};

				testSession.Store(comments);
				post.CommentsId = comments.Id;

				testSession.SaveChanges();
			}


			post.PublishAt = DateTime.Today.AddDays(5);

			var postDto = post.MapTo<PostDto>();

			this.sut.SaveOrUpdate(postDto);

			this.sut.Session.SaveChanges();

			var retrievedComments = this.sut.Session.Load<ItemComments>(comments.Id);
			
			retrievedComments.Should().Not.Be.Null();
			retrievedComments.Item.Should().Not.Be.Null();
			retrievedComments.Item.ItemPublishedAt.Should().Be.EqualTo(post.PublishAt);
		}

		#endregion
	}
}