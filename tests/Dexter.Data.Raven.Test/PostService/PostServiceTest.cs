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
	using Dexter.Data.Raven.Services;
	using Dexter.Data.Raven.Test.PostService.Helpers;
	using Dexter.Entities;

	using Moq;

	using SharpTestsEx;

	using Xunit;

	public class PostServiceTest : RavenDbTestBase, IDisposable
	{
		#region Fields

		private readonly PostDataService sut;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Object" /> class.
		/// </summary>
		public PostServiceTest()
		{
			this.sut = new PostDataService(new Mock<ILog>().Object, this.DocumentStore.OpenSession());
		}

		#endregion

		#region Public Methods and Operators

		public new void Dispose()
		{
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

		#endregion
	}
}