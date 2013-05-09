#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			FuturePostsViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/01
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host.Model.Widget
{
	using Dexter.Shared.Dto;
	using Dexter.Shared.Result;
	using Dexter.Web.Core.Models;

	public class FuturePostsViewModel : WidgetModelBase
	{
		#region Public Properties

		public IPagedResult<PostDto> Posts { get; set; }

		#endregion
	}
}