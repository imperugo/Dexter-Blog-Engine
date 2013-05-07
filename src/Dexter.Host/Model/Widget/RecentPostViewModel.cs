#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RecentPostViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/07
// Last edit:	2013/05/07
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Model.Widget
{
	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.Web.Core.Models;

	public class RecentPostViewModel : WidgetModelBase
	{
		#region Public Properties

		public IPagedResult<PostDto> Posts { get; set; }

		#endregion
	}
}