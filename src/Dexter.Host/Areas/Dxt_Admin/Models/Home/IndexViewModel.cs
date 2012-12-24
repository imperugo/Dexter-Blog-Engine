#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IndexViewModel.cs
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

namespace Dexter.Host.Areas.Dxt_Admin.Models.Home
{
	using System.Collections.Generic;

	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.Web.Core.Models;

	public class IndexViewModel : DexterModelBase
	{
		#region Public Properties

		public IEnumerable<CategoryDto> Categories { get; set; }

		public IPagedResult<PostDto> FuturePosts { get; set; }

		public FastPostBinder Post { get; set; }

		#endregion
	}
}