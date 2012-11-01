#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterModelBase.cs
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

namespace Dexter.Web.Core.Models
{
	using System.Collections.Generic;

	using Dexter.Entities;

	public class DexterModelBase
	{
		#region Public Properties

		public virtual IEnumerable<CommentDto> RecentComment { get; internal set; }

		public virtual IEnumerable<PostDto> RecentPost { get; internal set; }

		#endregion
	}
}