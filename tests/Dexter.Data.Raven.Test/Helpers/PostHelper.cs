#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test.Helpers
{
	using System.Collections.Generic;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities;

	using FizzWare.NBuilder;

	internal static class PostHelper
	{
		#region Public Methods and Operators

		public static IList<Post> GetPosts(int numberOfDocument)
		{
			return Builder<Post>.CreateListOfSize(numberOfDocument)
			                    .All()
			                    .With(x => x.Id = 0)
			                    .With(x => x.CommentsId = null)
			                    .Build();
		}

		public static IList<PostDto> GetPostsDto(int numberOfDocument)
		{
			return Builder<PostDto>.CreateListOfSize(numberOfDocument)
			                       .All()
			                       .With(x => x.Id = 0)
			                       .Build();
		}

		#endregion
	}
}