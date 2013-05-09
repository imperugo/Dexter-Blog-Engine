#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IndexViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/27
// Last edit:	2013/04/27
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Account
{
	using System.Collections.Generic;

	using Dexter.Shared.Result;
	using Dexter.Web.Core.Models;

	public class IndexViewModel : DexterBackofficeModelBase
	{
		#region Public Properties

		public IPagedResult<User> Users { get; set; }

		#endregion
	}
}