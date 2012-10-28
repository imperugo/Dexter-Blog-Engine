#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BlogStatisticsIndex.cs
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

	using global::Raven.Client.Indexes;

	public class BlogStatisticsIndex : AbstractIndexCreationTask<ItemComments, BlogStatistics>
	{
		#region Constructors and Destructors

		public BlogStatisticsIndex()
		{
			this.Map = postComments => postComments.Select(postComment => new
				                                                              {
					                                                              PostsCount = 1, 
					                                                              CommentsCount = postComment.TotalApprovedComments
				                                                              });

			this.Reduce = results => results.GroupBy(result => "constant").Select(g => new
				                                                                           {
					                                                                           PostsCount = g.Sum(x => x.PostsCount), 
					                                                                           CommentsCount = g.Sum(x => x.CommentsCount)
				                                                                           });
		}

		#endregion
	}
}