#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ICategoryDataService.cs
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

namespace Dexter.Data
{
	public interface ICategoryDataService
	{
		#region Public Methods and Operators

		void DeleteCategory(string id, string newCategoryId);

		string SaveOrUpdate(string name, bool isDefault, string parentCategoryId, string categoryId = null);

		#endregion
	}
}