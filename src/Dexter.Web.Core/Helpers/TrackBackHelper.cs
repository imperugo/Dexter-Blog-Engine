#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TrackBackHelper.cs
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

namespace Dexter.Web.Core.Helpers
{
	using System.Text;
	using System.Web.Mvc;

	using Dexter.Dependency;
	using Dexter.Entities;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;

	public static class TrackBackHelper
	{
		#region Public Methods and Operators

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