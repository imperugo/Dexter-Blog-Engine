#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPostDataService.cs
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

namespace Dexter.Data
{
	using System.Collections.Generic;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;

	public interface IPostDataService
	{
		#region Public Methods and Operators

		void Delete(int id);

		IList<MonthDto> GetMonthsForPublishedPosts();

		PostDto GetPostByKey(int id);

		PostDto GetPostBySlug(string slug);

		IPagedResult<PostDto> GetPosts(int pageIndex, int pageSize, ItemQueryFilter filters);

		IPagedResult<PostDto> GetPostsByDate(int pageIndex, int pageSize, int year, int? month, int? day, ItemQueryFilter filters);

		IPagedResult<PostDto> GetPostsByTag(int pageIndex, int pageSize, string tag, ItemQueryFilter filters);

		IPagedResult<PostDto> GetPostsByCategory(int pageIndex, int pageSize, string categoryName, ItemQueryFilter filters);

		IList<TagDto> GetTopTagsForPublishedPosts(int numberOfTags);

		void SaveOrUpdate(PostDto item);

		void SaveTrackback(TrackBackDto trackBack, int itemId);

		IPagedResult<PostDto> Search(string term, int pageIndex, int pageSize, ItemQueryFilter filters);

		#endregion
	}
}