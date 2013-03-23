#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPageDataService.cs
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

namespace Dexter.Data
{
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;

	public interface IPageDataService
	{
		#region Public Methods and Operators

		void Delete(int id);

		void SaveOrUpdate(PageDto item);

		string[] GetAllSlugs();

		PageDto GetPageByKey(int id);

		PageDto GetPageBySlug(string slug);

		IPagedResult<PageDto> GetPages(int pageIndex, int pageSize, ItemQueryFilter filter);

		#endregion
	}
}