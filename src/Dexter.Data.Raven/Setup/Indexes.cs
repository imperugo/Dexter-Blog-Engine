using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dexter.Data.Raven.Setup
{
	using Dexter.Data.Raven.Domain;

	using global::Raven.Client;
	using global::Raven.Client.Indexes;

	internal class Indexes
	{
		public static void UpdateDatabaseIndexes(IDocumentStore store)
		{
			var postsBySlugIndex = store.DatabaseCommands.GetIndex("BlogPosts/BySlug");

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
	}
}
