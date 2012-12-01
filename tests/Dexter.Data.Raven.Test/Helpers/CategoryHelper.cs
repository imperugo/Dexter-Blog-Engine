#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/01
// Last edit:	2012/12/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test.Helpers
{
	using System.Collections.Generic;

	using Dexter.Data.Raven.Domain;

	using FizzWare.NBuilder;

	internal static class CategoryHelper
	{
		public static IList<Category> GetCategories(int numberOfDocument)
		{
			return Builder<Category>.CreateListOfSize(numberOfDocument)
							.All()
							.With(x => x.Id = null)
							.With(x => x.ParentId = null)
							.With(x => x.ChildrenIds = null)
							.With(x => x.IsDefault = false)
							.Build();
		}
	}
}