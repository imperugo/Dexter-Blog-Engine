#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IndexViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/24
// Last edit:	2012/12/26
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Page
{
	using Dexter.Shared.Dto;
	using Dexter.Shared.Result;
	using Dexter.Web.Core.Models;

	public class IndexViewModel : DexterBackofficeModelBase
	{
		#region Public Properties

		public IPagedResult<PageDto> Pages { get; set; }

		#endregion
	}
}