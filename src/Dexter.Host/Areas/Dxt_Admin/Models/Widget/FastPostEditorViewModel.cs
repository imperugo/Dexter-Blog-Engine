#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			FastPostEditorViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/20
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Widget
{
	using System.Collections.Generic;

	using Dexter.Shared.Dto;
	using Dexter.Host.Areas.Dxt_Admin.Models.Home;
	using Dexter.Web.Core.Models;

	public class FastPostEditorViewModel : DexterBackofficeModelBase
	{
		#region Constructors and Destructors

		public FastPostEditorViewModel()
		{
			this.Post = FastPostBinder.EmptyInstance();
		}

		#endregion

		#region Public Properties

		public IEnumerable<CategoryDto> Categories { get; set; }

		public FastPostBinder Post { get; set; }

		#endregion
	}
}