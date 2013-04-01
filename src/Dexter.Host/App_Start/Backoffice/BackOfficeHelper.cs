#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BackOfficeHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/31
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
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
	using Dexter.Entities.Result;
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

			AddComments ( sb, u, helper.ViewContext );
			//AddStats(sb, u, helper.ViewContext);
			AddSettigns(sb, u, helper.ViewContext);

			//Addbackup(sb, u, helper.ViewContext);
			AddUsers(sb, u, helper.ViewContext);

			sb.Append("</li>");
			sb.Append("</ul>");

			return MvcHtmlString.Create(sb.ToString());
		}

		public static MvcHtmlString PagedSummary<T>(this IPagedResult<T> result)
		{
			long total = result.TotalCount;
			int min = (result.PageIndex - 1) * result.PageSize;

			if (min == 0)
			{
				min = 1;
			}

			long max = ((result.PageIndex - 1) * result.PageSize) + result.PageSize;

			if (result.TotalCount < max)
			{
				max = result.TotalCount;
			}

			return new MvcHtmlString(string.Format("Results {0} - {1} out of {2}", min, max, total));
		}

		public static MvcHtmlString Pager<T>(this HtmlHelper helper, IPagedResult<T> result)
		{
			if (result.TotalPages == 1)
			{
				return new MvcHtmlString(string.Empty);
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("<ul class=\"controls-buttons\">");

			if (result.HasPreviousPage)
			{
				string previousImageUrl = helper.ViewContext.HttpContext.Server.MapPath("~/images/icons/fugue/navigation-180.png");
				sb.Append("<li>");
				sb.AppendFormat("<a href=\"{0}\" title=\"Previous\">", "#");
				sb.AppendFormat("<img src=\"{0}\" width=\"16\" height=\"16\">", previousImageUrl);
				sb.Append("Prev");
				sb.Append("</a>");
				sb.Append("</li>");
			}

			for (int i = 0; i < result.TotalPages; i++)
			{
				sb.AppendFormat("<li><a href=\"{0}\" title=\"Page {1}\"><b>{1}</b></a></li>", "#", i + 1);
			}

			if (result.HasNextPage)
			{
				string nextImageUrl = helper.ViewContext.HttpContext.Server.MapPath("~/images/icons/fugue/navigation.png");
				sb.Append("<li>");
				sb.AppendFormat("<a href=\"{0}\" title=\"Next\">", "#");
				sb.AppendFormat("<img src=\"{0}\" width=\"16\" height=\"16\">", nextImageUrl);
				sb.Append("Next");
				sb.Append("</a>");
				sb.Append("</li>");
			}

			sb.Append("<ul>");

			return new MvcHtmlString(sb.ToString());
		}

		public static MvcHtmlString Render(this HtmlHelper helper, int pageSize, int pageIndex, long totalCounts, string unorderedListClass, bool renderPreviousAndNext, string previousText, string nextText, string currentClass, string baseUrl)
		{
			StringBuilder sb = new StringBuilder();

			if (unorderedListClass.IsNullOrEmpty())
			{
				sb.Append("<ul>");
			}
			else
			{
				sb.AppendFormat("<ul class=\"{0}\">", unorderedListClass);
			}

			if (pageIndex > 1 && renderPreviousAndNext)
			{
				sb.AppendFormat("<li><a href=\"{0}\" title=\"Previous\">{1}</a></li>", "#", previousText);
			}

			for (int page = pageIndex; page < Math.Round(totalCounts / pageSize + 0.5) && page < pageIndex + pageSize; page++)
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
			bool isCurrent = RouteHelper.IsCurrentController("Dxt_Admin", "Home", viewContext);

			sb.AppendFormat("<li class=\"home {0}\">", isCurrent ? "current" : string.Empty);
			sb.AppendFormat("<a href=\"{0}\" title=\"Home\">Home</a></li>", urlBuilder.Admin.Home());
		}

		private static void AddPost(StringBuilder sb, IUrlBuilder urlBuilder, ViewContext viewContext)
		{
			bool isCurrent = RouteHelper.IsCurrentController("Dxt_Admin", "Post", viewContext) || RouteHelper.IsCurrentController("Dxt_Admin", "Category", viewContext) || RouteHelper.IsCurrentController("Dxt_Admin", "Page", viewContext);

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
			bool isCurrent = RouteHelper.IsCurrentController("Dxt_Admin", "Settings", viewContext);

			sb.AppendFormat("<li class=\"settings {0}\">", isCurrent ? "current" : string.Empty);
			sb.AppendFormat("<a href=\"{0}\" title=\"settings\">settings</a>", urlBuilder.Admin.EditConfiguration());
			sb.Append("<ul>");
			sb.AppendFormat("<li><a href=\"{0}\" title=\"Site Configuration.\">Site</a></li>", urlBuilder.Admin.EditConfiguration());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"SEO Configuration.\">SEO</a></li>", urlBuilder.Admin.EditSeoConfiguration());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"Tracking Configuration.\">Tracking</a></li>", urlBuilder.Admin.EditTrackingConfiguration());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"Comments Configuration.\">Comment</a></li>", urlBuilder.Admin.EditCommentsConfiguration());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"SMTP Configuration.\">Smtp</a></li>", urlBuilder.Admin.EditSmtpConfiguration());
			sb.AppendFormat("<li><a href=\"{0}\" title=\"Reading Configuration.\">Reading</a></li>", urlBuilder.Admin.EditReadingConfiguration());
			sb.Append("</ul>");
			sb.Append("</li>");
		}

		private static void AddUsers(StringBuilder sb, IUrlBuilder urlBuilder, ViewContext viewContext)
		{
			bool isCurrent = RouteHelper.IsCurrentController("Dxt_Admin", "Users", viewContext);

			sb.AppendFormat("<li class=\"users {0}\">", isCurrent ? "current" : string.Empty);
			sb.AppendFormat("<a href=\"{0}\" title=\"Users\">Users</a>", urlBuilder.Admin.EditConfiguration());
			sb.Append("<ul>");
			sb.AppendFormat("<li><a href=\"{0}\" title=\"List.\">List</a></li>", "#");
			sb.AppendFormat("<li><a href=\"{0}\" title=\"Create.\">Create</a></li>", "#");
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
				sb.AppendFormat("<input type=\"checkbox\" name=\"{1}.Categories\" value=\"{0}\" {2} /> ", category.Name, bindePrefix, selectedValues != null && selectedValues.Contains(category.Name)
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