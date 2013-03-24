#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ConfirmDeleteViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/24
// Last edit:	2013/03/24
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Category
{
	using System.Collections.Generic;

	using Dexter.Entities;
	using Dexter.Web.Core.Models;

	public class ConfirmDeleteViewModel : DexterBackofficeModelBase
	{
		#region Public Properties

		public IEnumerable<CategoryDto> Categories { get; set; }

		public CategoryDto Category { get; set; }

		#endregion
	}
}