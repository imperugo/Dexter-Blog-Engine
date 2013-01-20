#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			WpCategoryInfo.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/11
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Web.Core.MetaWeblogApi.Domain
{
	using System;

	[Serializable]
	public class WpCategoryInfo
	{
		#region Fields

		public int categoryId;

		public string categoryName;

		public string description;

		public string htmlUrl;

		public int parentId;

		public string rssUrl;

		#endregion
	}
}