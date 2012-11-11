#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ArchiveViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/11/11
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Model.TagController
{
	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.Web.Core.Models;

	public class ArchiveViewModel : DexterModelBase
	{
		#region Public Properties

		public IPagedResult<PostDto> Posts { get; set; }

		public string Tag { get; set; }

		#endregion
	}
}