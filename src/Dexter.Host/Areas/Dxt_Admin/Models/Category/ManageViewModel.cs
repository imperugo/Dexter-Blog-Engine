#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ManageViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/23
// Last edit:	2013/03/23
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Category
{
	using System.Collections.Generic;

	using Dexter.Shared.Dto;
	using Dexter.Host.Areas.Dxt_Admin.Binders;
	using Dexter.Web.Core.Models;

	public class ManageViewModel : DexterBackofficeModelBase
	{
		#region Public Properties

		public IEnumerable<CategoryDto> Categories { get; set; }

		public CategoryBinder Category { get; set; }

		#endregion
	}
}