#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IndexViewModel.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/11
// Last edit:	2013/05/08
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Model.PageController
{
	using Dexter.Shared.Dto;
	using Dexter.Web.Core.Models;

	public class IndexViewModel : DexterModelBase
	{
		private PageDto page;

		#region Public Properties

		public PageDto Page
		{
			get
			{
				return this.page;
			}
			set
			{
				if (value != null)
				{
					this.ApplySeoInfoFromItem(value);
				}

				this.page = value;
			}
		}

		#endregion
	}
}