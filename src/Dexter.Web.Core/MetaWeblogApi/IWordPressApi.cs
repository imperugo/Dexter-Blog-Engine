#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IWordPressApi.cs
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

namespace Dexter.Web.Core.MetaWeblogApi
{
	using CookComputing.XmlRpc;

	using Dexter.Web.Core.MetaWeblogApi.Domain;

	public interface IWordPressApi
	{
		#region Public Methods and Operators

		[XmlRpcMethod("wp.deletePage", Description = "Removes a page from the blog.")]
		bool deletePage(
			string blog_id, 
			string username, 
			string password, 
			string page_id);

		[XmlRpcMethod("wp.editPage", Description = "Adds a new page/article to the blog engine.")]
		int editPage(
			string blog_id, 
			string page_id, 
			string username, 
			string password, 
			Page content, 
			bool publish);

		[XmlRpcMethod("wp.getPage", Description = "Get the page identified by the page id.")]
		Post getPage(
			string blog_id, 
			string page_id, 
			string username, 
			string password);

		[XmlRpcMethod("wp.getPageList", Description = "Get an array of all the pages on a blog. Just the minimum details.")]
		PageInfo[] getPageList(
			string blog_id, 
			string username, 
			string password);

		[XmlRpcMethod("wp.getPages", Description = "Get an array of all the pages on a blog.")]
		Page[] getPages(
			string blog_id, 
			string username, 
			string password);

		[XmlRpcMethod("wp.newCategory", Description = "Adds a new category to the blog engine.")]
		int newCategory(
			string blogid, 
			string username, 
			string password, 
			WpNewCategory category);

		[XmlRpcMethod("wp.newPage", Description = "Adds a new page/article to the blog engine.")]
		int newPage(
			string blog_id, 
			string username, 
			string password, 
			Page content, 
			bool publish);

		#endregion
	}
}