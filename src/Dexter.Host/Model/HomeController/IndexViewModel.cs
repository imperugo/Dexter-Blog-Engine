#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IndexViewModel.cs
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

namespace Dexter.Host.Model.HomeController
{
	using System.Collections.Generic;

	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.Web.Core.Models;

	public class IndexViewModel : DexterModelBase
	{
		public IPagedResult<PostDto> Posts { get; set; }
	}
}