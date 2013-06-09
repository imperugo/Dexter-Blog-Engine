#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPostService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services
{
	using System;
	using System.Collections.Generic;

	using Dexter.Dependency.Attributes;
	using Dexter.Dependency.Validator;
	using Dexter.Shared.Dto;
	using Dexter.Services.Events;
	using Dexter.Shared.Filters;
	using Dexter.Shared.Requests;
	using Dexter.Shared.Result;

	public interface IPostService : IValidate
	{
		#region Public Events

		/// <summary>
		/// This event will raise after to retrieve <see cref="MonthDto"/> of <see cref="PostDto"/> with specific filters. The event is raised by by the implementation of <see cref="GetMonthsForPublishedPosts"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IList<MonthDto>>> MonthsRetrievedForPublishedPosts;

		/// <summary>
		/// This event will raise before to retrieve <see cref="MonthDto"/> of <see cref="PostDto"/> by tag with specific filters. The event is raised by by the implementation of <see cref="GetMonthsForPublishedPosts"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithoutParameters<IList<MonthDto>>> MonthsRetrievingForPublishedPosts;

		/// <summary>
		/// This event will raise after the <see cref="PostDto"/> is delete. The event is raised by by the implementation of <see cref="Delete"/> or the async version.
		/// </summary>
		event EventHandler<EventArgs> PostDeleted;

		/// <summary>
		/// This event will raise before to delete <see cref="PostDto"/>. The event is raised by by the implementation of <see cref="Delete"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameterWithoutResult<int>> PostDeleting;

		/// <summary>
		/// This event will raise after to retrieve <see cref="PostDto"/> by specific key. The event is raised by by the implementation of <see cref="GetPostByKey"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<PostDto>> PostRetrievedById;

		/// <summary>
		/// This event will raise after to retrieve <see cref="PostDto"/> by specific slug. The event is raised by by the implementation of <see cref="GetPostBySlug"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<PostDto>> PostRetrievedBySlug;

		/// <summary>
		/// This event will raise before to retrieve <see cref="PostDto"/> by specific key. The event is raised by by the implementation of <see cref="GetPostByKey"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<int, PostDto>> PostRetrievingById;

		/// <summary>
		/// This event will raise before to retrieve <see cref="PostDto"/> by specific slug. The event is raised by by the implementation of <see cref="GetPostBySlug"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<string, PostDto>> PostRetrievingBySlug;

		/// <summary>
		/// This event will raise after the <see cref="PostDto"/> is saved if the property <see cref="ItemDto.Status"/> == <see cref="ItemStatus.Published"/>. The event is raised by by the implementation of <see cref="SaveOrUpdate"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithoutParameterWithResult<PostDto>> PostPublished;

		/// <summary>
		/// This event will raise after the <see cref="PostDto"/> is saved. The event is raised by by the implementation of <see cref="SaveOrUpdate"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithoutParameterWithResult<PostDto>> PostSaved;

		/// <summary>
		/// This event will raise before to save <see cref="PostDto"/>. The event is raised by by the implementation of <see cref="SaveOrUpdate"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<PostRequest, PostDto>> PostSaving;

		/// <summary>
		/// This event will raise after to retrieve <see cref="PostDto"/> filtered by the specified filters.. The event is raised by by the implementation of <see cref="GetPostsByDate"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedByDates;

		/// <summary>
		/// This event will raise after to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> by tag with specific filters. The event is raised by by the implementation of <see cref="GetPostsByTag"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedByTag;

		/// <summary>
		/// This event will raise after to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> with specific filters. The event is raised by by the implementation of <see cref="GetPosts"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedWithFilters;

		/// <summary>
		/// This event will raise before to retrieve <see cref="PostDto"/> filtered by the specified filters. The event is raised by by the implementation of <see cref="GetPostsByDate"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, int, int?, int?, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingByDates;

		/// <summary>
		/// This event will raise before to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> by tag with specific filters. The event is raised by by the implementation of <see cref="GetPostsByTag"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, string, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingByTag;

		/// <summary>
		/// This event will raise after to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> by tag with specific filters. The event is raised by by the implementation of <see cref="GetPostsByCategory"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedByCategory;

		/// <summary>
		/// This event will raise before to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> by tag with specific filters. The event is raised by by the implementation of <see cref="GetPostsByCategory"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, string, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingByCategory;

		/// <summary>
		/// This event will raise before to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> with specific filters. The event is raised by by the implementation of <see cref="GetPosts"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingWithFilters;

		/// <summary>
		/// This event will raise after to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> with specific filters. The event is raised by by the implementation of <see cref="Search"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsSearchedWithFilters;

		/// <summary>
		/// This event will raise before to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> with specific filters. The event is raised by by the implementation of <see cref="Search"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<Tuple<string, int, int, ItemQueryFilter>, IPagedResult<PostDto>>> PostsSearchingWithFilters;

		/// <summary>
		/// This event will raise after to retrieve <see cref="TagDto"/> of <see cref="PostDto"/> with specific filters. The event is raised by by the implementation of <see cref="GetTopTagsForPublishedPosts"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IList<TagDto>>> TagsRetrievedForPublishedPosts;

		/// <summary>
		/// This event will raise before to retrieve <see cref="TagDto"/> of <see cref="PostDto"/> by tag with specific filters. The event is raised by by the implementation of <see cref="GetTopTagsForPublishedPosts"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<int, IList<TagDto>>> TagsRetrievingForPublishedPosts;


		/// <summary>
		/// This event will raise after to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> by tag with specific filters. The event is raised by by the implementation of <see cref="GetPostsByAuthor"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedByAuthor;

		/// <summary>
		/// This event will raise before to retrieve <see cref="IPagedResult"/> of <see cref="PostDto"/> by tag with specific filters. The event is raised by by the implementation of <see cref="GetPostsByAuthor"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, string, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingByAuthor;

		#endregion

		#region Public Methods and Operators

		void Delete(int key);

		IList<MonthDto> GetMonthsForPublishedPosts();

		PostDto GetPostByKey(int key);

		PostDto GetPostBySlug(string slug);

		IPagedResult<PostDto> GetPosts(int pageIndex, int pageSize, ItemQueryFilter filters = null);

		IPagedResult<PostDto> GetPostsByAuthor(string username, int pageIndex, int pageSize, ItemQueryFilter filters);

		IPagedResult<PostDto> GetPostsByDate(int pageIndex, int pageSize, int year, int? month, int? day, ItemQueryFilter filters = null);

		IPagedResult<PostDto> GetPostsByTag(int pageIndex, int pageSize, string tag, ItemQueryFilter filters = null);

		IPagedResult<PostDto> GetPostsByCategory(int pageIndex, int pageSize, string categoryName, ItemQueryFilter filters = null);

		IList<TagDto> GetTopTagsForPublishedPosts(int maxNumberOfTags);

		PostDto SaveOrUpdate([Validate] PostRequest item);

		IPagedResult<PostDto> Search(string term, int pageIndex, int pageSize, ItemQueryFilter filters);

		#endregion
	}
}