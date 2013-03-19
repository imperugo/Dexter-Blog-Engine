#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ManageViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/26
// Last edit:	2012/12/26
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Page
{
	using System.Collections.Generic;

	using Dexter.Entities;
	using Dexter.Host.App_Start;
	using Dexter.Host.Areas.Dxt_Admin.Binders;
	using Dexter.Web.Core.Models;

	public class ManageViewModel : DexterBackofficeModelBase
	{
		#region Public Properties

		public string[] Hours
		{
			get
			{
				return Constants.HoursValues;
			}
		}

		public string[] Minutes
		{
			get
			{
				return Constants.MinutesValues;
			}
		}

		public PageBinder Page { get; set; }

		#endregion
	}
}