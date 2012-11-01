#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPostService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services
{
	using System;
	using System.Threading.Tasks;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Services.Events;

	public interface IPostService
	{
		#region Public Events

		/// <summary>
		/// This event will raise after to retrieve <see cref="PostDto"/> by specific key. The event is raised by by the implementation of <see cref="GetPostByKey"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<PostDto>> PostRetrievedById;

		/// <summary>
		/// This event will raise before to retrieve <see cref="PostDto"/> by specific key. The event is raised by by the implementation of <see cref="GetPostByKey"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<int, PostDto>> PostRetrievingById;

		/// <summary>
		/// This event will raise after to retrieve <see cref="PostDto"/> by specific slug. The event is raised by by the implementation of <see cref="GetPostBySlug"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<PostDto>> PostRetrievedBySlug;

		/// <summary>
		/// This event will raise before to retrieve <see cref="PostDto"/> by specific slug. The event is raised by by the implementation of <see cref="GetPostBySlug"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<string, PostDto>> PostRetrievingBySlug;

		/// <summary>
		/// This event will raise after to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> with specific filters. The event is raised by by the implementation of <see cref="GetPosts"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedWithFilters;

		/// <summary>
		/// This event will raise before to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> with specific filters. The event is raised by by the implementation of <see cref="GetPosts"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, PostQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingWithFilters;


		#endregion

		#region Public Methods and Operators

		PostDto GetPostByKey(int key);

		Task<PostDto> GetPostByKeyAsync(int key);

		PostDto GetPostBySlug(string slug);

		Task<PostDto> GetPostBySlugAsync(string slug);

		IPagedResult<PostDto> GetPosts(int pageIndex, int pageSize, PostQueryFilter filters);

		Task<IPagedResult<PostDto>> GetPostsAsync(int pageIndex, int pageSize, PostQueryFilter filter);

		#endregion
	}
}