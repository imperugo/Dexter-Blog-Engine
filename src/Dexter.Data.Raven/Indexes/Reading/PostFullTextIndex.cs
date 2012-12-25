#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostFullTextIndex.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/24
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Indexes.Reading
{
	using System;
	using System.Linq;

	using Dexter.Data.Raven.Domain;

	using global::Raven.Abstractions.Indexing;
	using global::Raven.Client.Indexes;

	public class PostFullTextIndex : AbstractIndexCreationTask<Post, PostFullTextIndex.ReduceResult>
	{
		#region Constructors and Destructors

		public PostFullTextIndex()
		{
			this.Map = posts => posts.Select(post => new
				                                         {
															 SearchQuery = post.Tags.Concat(new[]
																                                 {
																	                                 post.Title.ToLowerInvariant(),
																									 post.SearchContent
																                                 }),
					                                         PublishDate = post.PublishAt
				                                         });

			this.Index(x => x.SearchQuery, FieldIndexing.Analyzed);
		}

		#endregion

		public class ReduceResult
		{
			#region Public Properties

			public DateTimeOffset PublishDate { get; set; }

			public object[] SearchQuery { get; set; }

			#endregion
		}
	}
}