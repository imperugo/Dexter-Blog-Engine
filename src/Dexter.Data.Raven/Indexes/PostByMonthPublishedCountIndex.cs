#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostByMonthPublishedCountIndex.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/29
// Last edit:	2012/10/29
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Indexes
{
	using System.Linq;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities;

	using global::Raven.Abstractions.Indexing;

	using global::Raven.Client.Indexes;

	public class PostByMonthPublishedCountIndex : AbstractIndexCreationTask<Post, ArchiveResult>
	{
		#region Constructors and Destructors

		public PostByMonthPublishedCountIndex()
		{
			this.Map = posts => posts.Select(post => new
				                                         {
					                                         post.PublishAt.Year, 
					                                         post.PublishAt.Month, 
					                                         Count = 1
				                                         });
			this.Reduce = results => results.GroupBy(result => new
				                                                   {
					                                                   result.Year, 
					                                                   result.Month
				                                                   }).Select(g => new
					                                                                  {
						                                                                  g.Key.Year, 
						                                                                  g.Key.Month, 
						                                                                  Count = g.Sum(x => x.Count)
					                                                                  });

			this.Sort(x => x.Month, SortOptions.Int);
			this.Sort(x => x.Year, SortOptions.Int);
		}

		#endregion
	}
}