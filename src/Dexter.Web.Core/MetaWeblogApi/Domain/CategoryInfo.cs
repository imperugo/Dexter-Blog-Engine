#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryInfo.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/11/11
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.MetaWeblogApi.Domain
{
	using System;

	using CookComputing.XmlRpc;

	[Serializable]
	public class CategoryInfo
	{
		#region Fields

		public string categoryid;

		public string description;

		public string htmlUrl;

		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string parentid;

		public string rssUrl;

		public string title;

		#endregion
	}
}