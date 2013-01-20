#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPageService.cs
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
	using System.Threading.Tasks;

	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Services.Events;

	public interface IPageService
	{
		#region Public Events

		/// <summary>
		/// This event will raise after to retrieve <see cref="PageDto"/> by specific key. The event is raised by by the implementation of <see cref="GetPageByKey"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<PageDto>> PageRetrievedById;

		/// <summary>
		/// This event will raise after to retrieve <see cref="PageDto"/> by specific slug. The event is raised by by the implementation of <see cref="GetPageBySlug"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<PageDto>> PageRetrievedBySlug;

		/// <summary>
		/// This event will raise before to retrieve <see cref="PageDto"/> by specific key. The event is raised by by the implementation of <see cref="GetPageByKey"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<int, PageDto>> PageRetrievingById;

		/// <summary>
		/// This event will raise before to retrieve <see cref="PageDto"/> by specific slug. The event is raised by by the implementation of <see cref="GetPageBySlug"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<string, PageDto>> PageRetrievingBySlug;

		/// <summary>
		/// This event will raise after to retrieve <see cref="IPagedResult"/> of <see cref="PageDto"/> with specific filters. The event is raised by by the implementation of <see cref="GetPages"/> or the async version.
		/// </summary>
		event EventHandler<GenericEventArgs<IPagedResult<PageDto>>> PagesRetrievedWithFilters;

		/// <summary>
		/// This event will raise before to retrieve <see cref="IPagedResult"/> of <see cref="PageDto"/> with specific filters. The event is raised by by the implementation of <see cref="GetPages"/> or the async version.
		/// </summary>
		event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PageDto>>> PagesRetrievingWithFilters;

		#endregion

		#region Public Methods and Operators

		PageDto GetPageByKey(int key);

		Task<PageDto> GetPageByKeyAsync(int key);

		PageDto GetPageBySlug(string slug);

		Task<PageDto> GetPageBySlugAsync(string slug);

		IPagedResult<PageDto> GetPages(int pageIndex, int pageSize, ItemQueryFilter filters);

		Task<IPagedResult<PageDto>> GetPagesAsync(int pageIndex, int pageSize, ItemQueryFilter filter);

		#endregion
	}
}