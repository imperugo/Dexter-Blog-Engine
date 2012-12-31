#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BackOfficeHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/31
// Last edit:	2012/12/31
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Mvc.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Web;
	using System.Web.Mvc;

	using Dexter.Dependency;
	using Dexter.Entities;
	using Dexter.Navigation.Contracts;

	public static class BackOfficeHelper
	{
		#region Public Methods and Operators

		public static MvcHtmlString CategoriesStructure(this HtmlHelper helper, IList<CategoryDto> categories, string bindePrefix, string[] selectedValues)
		{
			StringBuilder sb = new StringBuilder();

			int categoryNumber = 0;

			CreateUnorderedList(categories, sb, ref categoryNumber, bindePrefix, selectedValues);

			return MvcHtmlString.Create(sb.ToString());
		}

		public static MvcHtmlString CreateMenu(this HtmlHelper helper)
		{
			IUrlBuilder u = DexterContainer.Resolve<IUrlBuilder>();

			StringBuilder sb = new StringBuilder();

			sb.Append("<ul class=\"container_12\">");

			AddHome(sb, u, helper.ViewContext);
			AddPost(sb, u, helper.ViewContext);

			//AddComments ( sb, u, helper.ViewContext );
			//AddStats(sb, u, helper.ViewContext);
			AddSettigns(sb, u, helper.ViewContext);

			//Addbackup(sb, u, helper.ViewContext);
			//AddUser(sb, u, helper.ViewContext);

			sb.Append("</li>");
			sb.Append("</ul>");

			return MvcHtmlString.Create(sb.ToString());
		}

		public static MvcHtmlString Render(this HtmlHelper helper, int pageSize, int pageIndex, long totalCounts, string ulClass, bool renderPreviousAndNext, string previousText, string nextText, string currentClass, string baseUrl)
		{
			IUrlBuilder u = DexterContainer.Resolve<IUrlBuilder>();

			StringBuilder sb = new StringBuilder();

			if (ulClass.IsNullOrEmpty())
			{
				sb.Append("<ul>");
			}
			else
			{
				sb.AppendFormat("<ul class=\"{0}\">", ulClass);
			}

			if (pageIndex > 1 && renderPreviousAndNext)
			{
				sb.AppendFormat("<li><a href=\"{0}\" title=\"Previous\">{1}</a></li>", "#", previousText);
			}

			for (int page = pageIndex; page < Math.Round((totalCounts / pageSize) + 0.5) && page < pageIndex + pageSize; page++)
			{
				sb.AppendFormat(page == pageIndex
					                ? "<li><a href=\"{0}\" title=\"Page {1}\"><b>{1}</b></a></li>"
					                : "<li><a href=\"{0}\" title=\"Page {1}\" class=\"current\"><b>{1}</b></a></li>", "#", page + 1);
			}

			if ((pageIndex * pageSize) <= totalCounts)
			{
				sb.AppendFormat("<li><a href=\"{0}\" title=\"Next\">{1}</a></li>", "#", nextText);
			}

			sb.Append("<ul>");

			return MvcHtmlString.Create(sb.ToString());
		}

		public static MvcHtmlString RenderBackOffice(this HtmlHelper helper, int pageSize, int pageIndex, long totalCounts, string baseUrl)
		{
			string prevImageString = string.Format("<img src=\"{0}\" width=\"16\" height=\"16\"> Prev", VirtualPathUtility.ToAbsolute("~/admin/Resources/images/Icons/Fugue/navigation-180.png"));
			string nextImageString = string.Format("Next <img src=\"{0}\" width=\"16\" height=\"16\">", VirtualPathUtility.ToAbsolute("~/admin/Resources/images/Icons/Fugue/navigation.png"));

			return Render(helper, pageSize, 
				pageIndex, 
				totalCounts, 
				"controls-buttons", 
				true, 
				prevImageString, 
				nextImageString, 
				"current", 
				baseUrl);
		}

		#endregion

		#region Methods

		private static void AddComments(StringBuilder sb, IUrlBuilder urlBuilder, ViewContext viewContext)
		{
			sb.Append("<li class=\"comments\">");
			sb.AppendFormat("<a href=\"{0}\" title=\"Comments\">Comments</a>", urlBuilder.Admin.Home());
			sb.Append("<ul>");
			sb.Append("</ul>");
			sb.Append("</li>");
		}

		private static void AddHome(StringBuilder sb, IUrlBuilder urlBuilder, ViewContext viewContext)
		{
			bool isCurrent = RouteHelper.IsCurrentController("Admin", "Home", viewContext);

			sb.AppendFormat("<li class=\"home {0}\">", isCurrent ? "current" : string.Empty);
			sb.AppendFormat("<a href=\"{0}\" title=\"Home\">Home</a></li>", urlBuilder.Admin.Home());
		}

		private static void AddPost(StringBuilder sb, IUrlBuilder urlBuilder, ViewContext viewContext)
		{
			bool isCurrent = RouteHelper.IsCurrentController("Admin", "Post", viewContext) || RouteHelper.IsCurrentController("Admin", "Category", viewContext) || RouteHelper.IsCurrentController("Admin", "Page", viewContext);

			sb.AppendFormat("<li class=\"write {0}\">", isCurrent ? "current" : string.Empty);
			sb.AppendFormat("<a href=\"{0}\" title=\"Post & Pages\">Posts</a>", urlBuilder.Admin.Post.List());
			sb.Append("<ul>");
			sb.AppendFormat("<li><a href=\"{0}\" title=\"All Posts.\">Posts</a></li>", urlBuilder.Admin.Post.List());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"Add new post.\">Add Post</a></li>", urlBuilder.Admin.Post.New());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"All pages.\">Pages</a></li>", urlBuilder.Admin.Page.List());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"Add new page.\">Add Page</a></li>", urlBuilder.Admin.Post.New());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"All categories.\">Categories</a></li>", urlBuilder.Admin.Category.List());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"Add new category.\">Add Category</a></li>", urlBuilder.Admin.Category.New());
			sb.Append("</ul>");
			sb.Append("</li>");
		}

		private static void AddSettigns(StringBuilder sb, IUrlBuilder urlBuilder, ViewContext viewContext)
		{
			bool isCurrent = RouteHelper.IsCurrentController("Admin", "Settings", viewContext);

			sb.AppendFormat("<li class=\"settings {0}\">", isCurrent ? "current" : string.Empty);
			sb.AppendFormat("<a href=\"{0}\" title=\"settings\">settings</a>", urlBuilder.Admin.EditConfiguration());
			sb.Append("<ul>");
			sb.AppendFormat("<li><a href=\"{0}\" title=\"Site Configuration.\">Site Configuration</a></li>", urlBuilder.Admin.EditConfiguration());
			sb.Append("</ul>");
			sb.Append("</li>");
		}

		private static void CreateUnorderedList(IList<CategoryDto> categories, StringBuilder sb, ref int categoryNumber, string bindePrefix, IEnumerable<string> selectedValues)
		{
			if (categories == null || !categories.Any())
			{
				return;
			}

			sb.Append("<ul class=\"collapsible-list with-bg\">");

			foreach (CategoryDto category in categories)
			{
				sb.Append("<li class=\"closed\">");

				sb.Append(category.Categories.Any()
					          ? "<b class=\"toggle\"></b>"
					          : "<b></b>");

				sb.Append("<span>");
				sb.AppendFormat("<input type=\"hidden\" name=\"{2}.Categories[{0}].ID\" value=\"{1}\" />", categoryNumber, category.Id, bindePrefix);
				sb.AppendFormat("<input type=\"checkbox\" name=\"{2}.Categories[{0}].Name\" value=\"{1}\" {3} /> ", categoryNumber, category.Name, bindePrefix, selectedValues != null && selectedValues.Contains(category.Id)
					                                                                                                                                                ? "checked=\"checked\""
					                                                                                                                                                : string.Empty);
				sb.Append(category.Name);
				sb.Append("</span>");

				categoryNumber++;

				CreateUnorderedList(category.Categories, sb, ref categoryNumber, bindePrefix, selectedValues);
			}

			sb.Append("</ul>");
		}

		#endregion
	}
}