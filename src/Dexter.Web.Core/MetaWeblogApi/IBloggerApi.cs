#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IBloggerApi.cs
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

namespace Dexter.Web.Core.MetaWeblogApi
{
	using CookComputing.XmlRpc;

	using Dexter.Web.Core.MetaWeblogApi.Domain;

	public interface IBloggerApi
	{
		#region Public Methods and Operators

		[XmlRpcMethod("blogger.deletePost", Description = "Deletes a post.")]
		[return: XmlRpcReturnValue(Description = "Always returns true.")]
		bool deletePost(
			string appKey, 
			string postid, 
			string username, 
			string password, 
			[XmlRpcParameter(
				Description = "Where applicable, this specifies whether the blog should be republished after the post has been deleted.")] bool publish);

		[XmlRpcMethod("blogger.getUsersBlogs", Description = "Returns information on all the blogs a given user is a member.")]
		BlogInfo[] getUsersBlogs(
			string appKey, 
			string username, 
			string password);

		#endregion
	}
}