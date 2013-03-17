#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TagHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/17
// Last edit:	2013/03/17
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Shared.Helpers
{
	using System;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Web;

	public static class TagHelper
	{
		#region Public Methods and Operators

		public static string[] RetrieveTagsFromBody(string html)
		{
			Regex relRegex = new Regex(@"\s+rel\s*=\s*(""[^""]*?\btag\b.*?""|'[^']*?\btag\b.*?')", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Regex hrefRegex = new Regex(@"\s+href\s*=\s*(""(?<url>[^""]*?)""|'(?<url>[^']*?)')", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Regex anchorRegex = new Regex(@"<a(\s+\w+\s*=\s*(?:""[^""]*?""|'[^']*?')(?!\w))+\s*>.*?</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);

			Collection<string> tags = new Collection<string>();
			Collection<string> loweredTags = new Collection<string>();

			foreach (Match m in anchorRegex.Matches(html))
			{
				string anchorHtml = m.Value;
				if (!relRegex.IsMatch(anchorHtml))
				{
					continue;
				}

				Match urlMatch = hrefRegex.Match(anchorHtml);
				if (urlMatch.Success)
				{
					string urlStr = urlMatch.Groups["url"].Value;
					if (urlStr.EndsWith("/default.aspx", StringComparison.InvariantCultureIgnoreCase))
					{
						urlStr = urlStr.Substring(0, urlStr.Length - 13);
					}
					Uri url;
					if (Uri.TryCreate(urlStr, UriKind.RelativeOrAbsolute, out url))
					{
						string[] seg = url.Segments;
						string tag = HttpUtility.UrlDecode(seg[seg.Length - 1].Replace("/", string.Empty));

						//Keep a list of lowered tags so we can prevent duplicates without modifying capitalization
						string loweredTag = tag.ToLower();
						if (!loweredTags.Contains(loweredTag))
						{
							loweredTags.Add(loweredTag);
							tags.Add(tag);
						}
					}
				}
			}
			return tags.ToArray();
		}

		#endregion
	}
}