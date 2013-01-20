#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostDataServiceTest.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/27
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test.PostService
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using global::AutoMapper;

	using Common.Logging;

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Services;
	using Dexter.Data.Raven.Test.Helpers;
	using Dexter.Entities;
	using Dexter.Entities.Result;

	using Moq;

	using global::Raven.Client;

	using SharpTestsEx;

	using Xunit;

	public class PostDataServiceTest : RavenDbTestBase, IDisposable
	{
		#region Fields

		private readonly TestSessionFactory sessionFactory;

		private readonly PostDataService sut;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Object" /> class.
		/// </summary>
		public PostDataServiceTest()
		{
			this.sessionFactory = new TestSessionFactory(this.DocumentStore);
			this.sut = new PostDataService(new Mock<ILog>().Object, this.sessionFactory, this.DocumentStore);
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
		public void SavePost_ChangingSlug_ShouldUpdateDenormalizedData()
		{
			Post post;
			ItemComments comments;

			using (IDocumentSession testSession = this.DocumentStore.OpenSession())
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

			PostDto postDto = post.MapTo<PostDto>();

			this.sut.SaveOrUpdate(postDto);

			this.sut.Session.SaveChanges();

			ItemComments retrievedComments = this.sut.Session.Load<ItemComments>(comments.Id);

			retrievedComments.Should().Not.Be.Null();
			retrievedComments.Item.Should().Not.Be.Null();
			retrievedComments.Item.ItemPublishedAt.Should().Be.EqualTo(post.PublishAt);
		}

		[Fact]
		public void SavePost_WithValidData_ShouldSaveThePostAndTheItemComments()
		{
			PostDto post = PostHelper.GetPostsDto(1)[0];

			this.sut.SaveOrUpdate(post);

			this.sut.Session.SaveChanges();

			post.Id.Should().Be.GreaterThan(0);

			Post postEntity = this.sut.Session
			                      .Include<Post>(x => x.CommentsId)
			                      .Load<Post>(post.Id);

			postEntity.Should().Not.Be.Null();
			postEntity.Title.Should().Be.EqualTo(post.Title);
			post.Slug.Should().Not.Be.Null();

			ItemComments itemComments = this.sut.Session.Load<ItemComments>(postEntity.CommentsId);

			itemComments.Should().Not.Be.Null();
		}

		[Fact]
		public void SearchPost_WithValidData_ShouldReturnValidItems()
		{
			IList<Post> posts = PostHelper.GetPosts(6);

			posts[0].Title = "Manage cookies using Web API";
			posts[0].SearchContent = "In my last project, I’ve deeply used WebApi, both for the client side and server side. In fact my application must call several REST endpoints developed with java and, after a code elaboration, I have to expose the data to other clients (javascript into an html page in this case).One of the pillars request by the Java services (really is not a technology request but just from the implementation made by the external company) is to read all cookies from the response and then send them back to the next requests (like a proxy).To make more clear where my app is, I realized the following chart:";

			posts[1].Title = "Different keys with RavenDb";
			posts[1].SearchContent = "In last period, I am spending so times to learn document databases, in my case RavenDB and MongoDB. To be honest I am working only on Raven right now because it is friendlier for .NET developers but I promised myself to compare some features from these two awesome database engines. One of the big difficult I found, is to create the right model. I said big because I’m used to design the model for relation database and here is really different. For example we do not have the join and we also need to denormalize the references (http://ravendb.net/docs/faq/denormalized-references).";

			posts[2].Title = "An amazing experience";
			posts[2].SearchContent = "Yesterday something special is happened and I still cannot believe that the Web.Net European Conference is over. For me was the first conference as a promoter and it was an AMAZING experience. More than 160 people from 11 different countries in the same building speaking about the future of the web (how F*ing cool is that?).";

			posts[3].Title = "Use Less, Sass and Compass with ASP.NET MVC";
			posts[3].SearchContent = "In my last project, with my team, we chose to use Compass with SASS as CSS Authoring Framework.  Initially we was unsecure about that, the choice was very hard, the classic CSS allows you to find several guys who know it and it doesn’t require compilation unlike with Sass and Less. I’m not a front end developer so my part in this adventure is to manage the project and to make easy the integration of Less/Sass with an ASP.NET MVC application.";

			posts[4].Title = "The best extensions for Visual Studio 2012";
			posts[4].SearchContent = "Visual Studio 2012 is absolutely the best IDE in the world and, with the latest version, it has sorted most of the big problems (from my point of view the previous version was a bit slow). I’m not a big fan of extensions because they often make Visual Studio unstable and/or slow, but I’ll make an exception because it this case it’s incredibly cool!";

			posts[5].Title = "How integrate Facebook, Twitter, LinkedIn, Yahoo, Google and Microsoft Account with your ASP.NET MVC application";
			posts[5].SearchContent = "In the past week, 15 august, Microsoft released an incredible number of cool stuff, starting from Windows 8 and ending to Visual Studio 2012 including the new ASP.NET Stack. The version 4 of ASP.NET MVC introduces several cool features; most of them was available with the Beta and RC versions (Web API, Bundling, Mobile Projects Templates, etc.), but the RTM is not a “fixed version” of the RC, it has other interesting things.";

			foreach (Post post in posts)
			{
				this.SetupData(x => x.Store(post));
			}

			this.WaitStaleIndexes();

			IPagedResult<PostDto> result = this.sut.Search("facebook", 1, 10, null);

			result.Result.Count().Should().Be.GreaterThan(0);
		}

		#endregion
	}
}