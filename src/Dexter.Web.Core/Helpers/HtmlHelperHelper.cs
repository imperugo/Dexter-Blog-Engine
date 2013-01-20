#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			HtmlHelperHelper.cs
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

namespace System.Web.Mvc.Html
{
	using System.Text;

	using Dexter.Dependency;
	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Web.Core.Models;

	public static class HtmlHelperHelper
	{
		#region Public Methods and Operators

		public static void RenderAction(this HtmlHelper helper, string actionName, string controller, string area)
		{
			helper.RenderAction(actionName, controller, new
				                                            {
					                                            area
				                                            });
		}

		public static DateTime ConvertToUserTimeZone(this DexterModelBase model, DateTimeOffset dateTimeOffset)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, model.ConfigurationDto.TimeZone).DateTime;
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

		#endregion
	}
}