#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Indexes.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/02
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Setup
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	using Dexter.Data.Raven.Domain;

	using global::Raven.Abstractions.Indexing;

	using global::Raven.Client;

	using global::Raven.Client.Indexes;

	public class Indexes
	{
		#region Public Methods and Operators

		public static void UpdateDatabaseIndexes(IDocumentStore store)
		{
			IndexDefinition postsBySlugIndex = store.DatabaseCommands.GetIndex("BlogPosts/BySlug");
			IndexDefinition categoryByDefault = store.DatabaseCommands.GetIndex("Category/ById");
			IndexDefinition postsIdsInCategoryByDefault = store.DatabaseCommands.GetIndex("Category/PostIds");
			IndexDefinition postsbyIds = store.DatabaseCommands.GetIndex("Category/UpdatePostsName");

			if (postsBySlugIndex == null)
			{
				store.DatabaseCommands.PutIndex("BlogPosts/BySlug", 
					new IndexDefinitionBuilder<Item>
						{
							Map = items => items.Select(post => new
								                                    {
									                                    post.Slug
								                                    })
						});
			}

			if (categoryByDefault == null)
			{
				store.DatabaseCommands.PutIndex("Category/ById", 
					new IndexDefinitionBuilder<Category>
						{
							Map = categories => categories.Select(category => new
								                                                  {
									                                                  category.Id
								                                                  })
						});
			}

			if (postsIdsInCategoryByDefault == null)
			{
				store.DatabaseCommands.PutIndex("Category/PostIds",
					new IndexDefinitionBuilder<Category>
					{
						Map = categories => categories.Select(category => new
						{
							category.Name
						})
					});
			}

			if (postsbyIds == null)
			{
				store.DatabaseCommands.PutIndex("Category/UpdatePostsName",
					new IndexDefinitionBuilder<Post>
					{
						Map = posts => posts.Select(post => new {post.Id})
					});
			}
		}

		#endregion
	}
}