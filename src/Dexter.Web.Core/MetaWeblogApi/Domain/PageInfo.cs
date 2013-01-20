#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PageInfo.cs
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

	using CookComputing.XmlRpc;

	/// <summary>
	/// 	minimal details
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public class PageInfo
	{
		#region Fields

		public DateTime dateCreated;

		public int page_id;

		public int page_parent_id;

		public string page_title;

		#endregion
	}
}