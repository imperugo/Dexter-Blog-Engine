#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			MtCategory.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/12/24
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
	public class MtCategory
	{
		#region Fields

		public string categoryId;

		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string categoryName;

		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public bool isPrimary;

		#endregion
	}
}