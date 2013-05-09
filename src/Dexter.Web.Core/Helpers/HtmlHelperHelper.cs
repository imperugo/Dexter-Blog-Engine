#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			HtmlHelperHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/31
// Last edit:	2013/04/27
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace System.Web.Mvc.Html
{
	using System.Text;

	using Dexter.Dependency;
	using Dexter.Shared.Dto;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Web.Core.Models;

	public static class HtmlHelperHelper
	{
		#region Public Methods and Operators

		public static DateTime ConvertToUserTimeZone(this DexterModelBase model, DateTimeOffset dateTimeOffset)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, model.ConfigurationDto.TimeZone).DateTime;
		}

		public static MvcHtmlString Favicon(this HtmlHelper helper)
		{
			UrlHelper uh = new UrlHelper(helper.ViewContext.RequestContext);

			string faviconUrl = uh.Content("~/Images/favicon.ico");

			return new MvcHtmlString(string.Format("<link href=\"{0}\" rel=\"shortcut icon\" type=\"image/x-icon\" />", faviconUrl));
		}

		public static MvcHtmlString FeedUrl(this HtmlHelper helper)
		{
			IUrlBuilder u = DexterContainer.Resolve<IUrlBuilder>();
			DexterModelBase model = (DexterModelBase)helper.ViewData.Model;

			return new MvcHtmlString(string.Format("<link href=\"{0}\" rel=\"alternate\" type=\"application/rss+xml\" title=\"{1}\" />", u.Feed.MainFeed(), model.Title));
		}

		public static MvcHtmlString HeadSuperPack(this HtmlHelper helper)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(helper.MetaDescription().ToString());
			sb.AppendLine(helper.MetaKeywords().ToString());
			sb.AppendLine(helper.MetaAuthor().ToString());
			sb.AppendLine(helper.MetaGenerator().ToString());
			sb.AppendLine(helper.Index().ToString());
			sb.AppendLine(helper.Favicon().ToString());
			sb.AppendLine(helper.WindowsLiveWriterManifest().ToString());
			sb.AppendLine(helper.MetaWeblogRsd().ToString());
			//sb.AppendLine(helper.OpenSearch().ToString());
			sb.AppendLine(helper.FeedUrl().ToString());
			//sb.AppendLine(helper.PingBack().ToString());

			return new MvcHtmlString(sb.ToString());
		}

		public static MvcHtmlString Index(this HtmlHelper helper)
		{
			IUrlBuilder u = DexterContainer.Resolve<IUrlBuilder>();
			DexterModelBase model = (DexterModelBase)helper.ViewData.Model;
			return new MvcHtmlString(string.Format("<link rel=\"index\" title=\"{0}\" href=\"{1}\" />", model.Title, u.Home));
		}

		public static MvcHtmlString MetaAuthor(this HtmlHelper helper)
		{
			DexterModelBase model = (DexterModelBase)helper.ViewData.Model;

			return new MvcHtmlString(string.Format("<meta name=\"author\" content=\"{0}\" />", model.Author));
		}

		public static MvcHtmlString MetaDescription(this HtmlHelper helper)
		{
			DexterModelBase model = (DexterModelBase)helper.ViewData.Model;

			return new MvcHtmlString(string.Format("<meta name=\"description\" content=\"{0}\" />", model.Description));
		}

		public static MvcHtmlString MetaGenerator(this HtmlHelper helper)
		{
			return new MvcHtmlString(string.Format("<meta name=\"generator\" content=\"{0}\" />", DexterInfo.Generator));
		}

		public static MvcHtmlString MetaKeywords(this HtmlHelper helper)
		{
			DexterModelBase model = (DexterModelBase)helper.ViewData.Model;

			return new MvcHtmlString(string.Format("<meta name=\"keywords\" content=\"{0}\" />", model.KeyWords));
		}

		public static MvcHtmlString MetaWeblogRsd(this HtmlHelper helper)
		{
			IUrlBuilder u = DexterContainer.Resolve<IUrlBuilder>();
			return new MvcHtmlString(string.Format("<link href=\"{0}\" rel=\"EditURI\" type=\"application/rsd+xml\" title=\"RSD\" />", u.Service.MetaWeblogRsd()));
		}

		public static MvcHtmlString OpenSearch(this HtmlHelper helper)
		{
			IUrlBuilder u = DexterContainer.Resolve<IUrlBuilder>();

			DexterModelBase model = (DexterModelBase)helper.ViewData.Model;
			return new MvcHtmlString(string.Format("<link href=\"{0}\" rel=\"search\" type=\"application/opensearchdescription+xml\" title=\"{1}\" />", u.Service.OpenSearch(), model.Title));
		}

		public static MvcHtmlString PingBack(this HtmlHelper helper)
		{
			IUrlBuilder u = DexterContainer.Resolve<IUrlBuilder>();

			return new MvcHtmlString(string.Format("<link href=\"{0}\" rel=\"pingback\" />", u.Service.Pingback()));
		}

		public static void RenderAction(this HtmlHelper helper, string actionName, string controller, string area)
		{
			helper.RenderAction(actionName, controller, new
				                                            {
					                                            area
				                                            });
		}

		public static MvcHtmlString TrackBackRdf(this HtmlHelper helper, PostDto item)
		{
			IUrlBuilder u = DexterContainer.Resolve<IUrlBuilder>();
			BlogConfigurationDto c = DexterContainer.Resolve<IConfigurationService>().GetConfiguration();

			if (!c.Tracking.EnableTrackBackReceive)
			{
				return MvcHtmlString.Empty;
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("<!--");
			sb.Append("<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:trackback=\"http://madskills.com/public/xml/rss/module/trackback/\">");
			sb.AppendLine();
			sb.AppendFormat("<rdf:Description rdf:about=\"{0}\" dc:identifier=\"{0}\" dc:title=\"{1}\" trackback:ping=\"{2}\" />", u.Post.Permalink(item), item.Title, u.Post.TrackBack(item));
			sb.AppendLine("</rdf:RDF>");
			sb.AppendLine("-->");

			return MvcHtmlString.Create(sb.ToString());
		}

		public static MvcHtmlString WindowsLiveWriterManifest(this HtmlHelper helper)
		{
			return new MvcHtmlString(@"<link rel=""wlwmanifest"" type=""application/wlwmanifest+xml"" href=""/wlwmanifest.xml""/>");
		}

		#endregion
	}
}