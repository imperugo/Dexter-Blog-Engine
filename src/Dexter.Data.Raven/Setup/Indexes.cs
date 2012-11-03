#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Indexes.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/11/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Setup
{
	using System.Linq;

	using Dexter.Data.Raven.Domain;

	using global::Raven.Abstractions.Data;
	using global::Raven.Abstractions.Indexing;

	using global::Raven.Client;

	using global::Raven.Client.Indexes;

	public class Indexes
	{
		#region Public Methods and Operators

		public static void UpdateDatabaseIndexes(IDocumentStore store)
		{
			IndexDefinition postsBySlugIndex = store.DatabaseCommands.GetIndex("BlogPosts/BySlug");

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
		}

		#endregion
	}
}