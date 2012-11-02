#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TagsForPublishedPostsWithCountIndex.cs
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

namespace Dexter.Data.Raven.Indexes
{
	using System.Globalization;
	using System.Linq;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities;

	using global::Raven.Client.Indexes;

	public class TagsForPublishedPostsWithCountIndex : AbstractIndexCreationTask<Post, TagDto>
	{
		#region Constructors and Destructors

		public TagsForPublishedPostsWithCountIndex()
		{
			this.Map = posts => posts.Where(x => x.Status == ItemStatus.Published)
				                    .SelectMany(post => post.Tags, (post, tag) => new
					                                                                  {
						                                                                  Name = tag.ToString(CultureInfo.InvariantCulture), 
						                                                                  Count = 1, 
						                                                                  LastSeenAt = post.PublishAt
					                                                                  });

			this.Reduce = results => results.GroupBy(tagCount => tagCount.Name)
				                         .Select(g => new
					                                      {
						                                      Name = g.Key, 
						                                      Count = g.Sum(x => x.Count), 
						                                      LastSeenAt = g.Max(x => x.LastSeenAt), 
					                                      });
		}

		#endregion
	}
}