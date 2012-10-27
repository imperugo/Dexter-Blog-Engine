#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPostService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/DexterBlogEngine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data
{
	using System;

	using Dexter.Data.DataTransferObjects;
	using Dexter.Data.DataTransferObjects.Result;

	public interface IPostService
	{
		#region Public Methods and Operators

		IPagedResult<PostDto> GetLastPost(int pageIndex, int pageSize, PostQueryFilter filter);

		PostDto GetPostDtoById(int id);

		PostDto GetPostDtoByKey(string slug);

		#endregion
	}

	public class PostQueryFilter
	{
		#region Public Properties

		public DateTimeOffset MaxPublishAt { get; set; }

		#endregion
	}
}