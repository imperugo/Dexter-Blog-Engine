#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Post.cs
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

	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public class Post
	{
		#region Fields

		[XmlRpcMissingMapping(MappingAction.Error)]
		[XmlRpcMember(Description = "Required when posting.")]
		public string[] categories;

		public string[] custom_fields;

		[XmlRpcMissingMapping(MappingAction.Error)]
		[XmlRpcMember(Description = "Required when posting.")]
		public DateTime dateCreated;

		public DateTime date_created_gmt;

		[XmlRpcMissingMapping(MappingAction.Error)]
		[XmlRpcMember(Description = "Required when posting.")]
		public string description;

		/// <summary>
		/// 	used by metaweblog
		/// </summary>
		public string link;

		/// <summary>
		/// 	int enumerated value
		/// 	0 - Default (same as none?)
		/// 	0 - None
		/// 	1 - Open
		/// 	2 - Close
		/// </summary>
		public int mt_allow_comments;

		/// <summary>
		/// 	todo: support post updates notification through pinging to various services (like weblogs.com or similar)
		/// </summary>
		public int mt_allow_pings;

		/// <summary>
		/// 	todo: implement support
		/// </summary>
		public string mt_convert_breaks;

		public string mt_excerpt;

		/// <summary>
		/// 	contains a list of tags passed from WLW
		/// </summary>
		public string mt_keywords;

		public string mt_text_more;

		/// <summary>
		/// 	used by metaweblog
		/// </summary>
		public string permalink;

		public string post_status;

		public string postid;

		public bool sticky;

		[XmlRpcMissingMapping(MappingAction.Error)]
		[XmlRpcMember(Description = "Required when posting.")]
		public string title;

		/// <summary>
		/// 	used by metaweblog
		/// </summary>
		public string userid;

		/// <summary>
		/// 	username of the author of the post
		/// </summary>
		public string wp_author_display_name;

		/// <summary>
		/// 	id of the post author (used by wlw)
		/// 
		/// 	null or empty means [Default] in wlw
		/// </summary>
		public string wp_author_id;

		public string wp_password;

		public string wp_slug;

		#endregion
	}
}