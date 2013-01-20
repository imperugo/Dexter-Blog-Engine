#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IndexViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/29
// Last edit:	2012/12/29
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Models.Feedback
{
	using System;

	using Dexter.Navigation.Contracts;
	using Dexter.Web.Core.Models;

	public class IndexViewModel : DexterBackofficeModelBase
	{
		#region Public Properties

		public FeedbackType FeedbackType { get; set; }

		public string Message { get; set; }

		public Uri Redirect { get; set; }

		#endregion
	}
}