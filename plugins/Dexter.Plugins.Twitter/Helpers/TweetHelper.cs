#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TweetHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/11
// Last edit:	2013/03/11
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Plugins.Twitter.Helpers
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;
	using System.Web;

	internal static class TweetHelper
	{
		#region Constants

		private const string HashTagPattern = @"#([A-Za-z0-9\-_&;]+)";

		private const string HyperLinkPattern = @"(http://\S+)\s?";

		private const string ScreenNamePattern = @"@([A-Za-z0-9\-_&;]+)";

		#endregion

		#region Public Methods and Operators

		public static string FormatTweetText(string text)
		{
			string result = text;

			if (result.Contains("http://"))
			{
				List<string> links = new List<string>();
				foreach (Match match in Regex.Matches(result, HyperLinkPattern))
				{
					string url = match.Groups[1].Value;
					if (!links.Contains(url))
					{
						links.Add(url);
						result = result.Replace(url, string.Format("<a href=\"{0}\">{0}</a>", url));
					}
				}
			}

			if (result.Contains("@"))
			{
				List<string> names = new List<string>();
				foreach (Match match in Regex.Matches(result, ScreenNamePattern))
				{
					string screenName = match.Groups[1].Value;
					if (!names.Contains(screenName))
					{
						names.Add(screenName);
						result = result.Replace("@" + screenName, string.Format("<a href=\"http://twitter.com/{0}\">@{0}</a>", screenName));
					}
				}
			}

			if (result.Contains("#"))
			{
				List<string> names = new List<string>();
				foreach (Match match in Regex.Matches(result, HashTagPattern))
				{
					string hashTag = match.Groups[1].Value;
					if (!names.Contains(hashTag))
					{
						names.Add(hashTag);
						result = result.Replace("#" + hashTag, 
							string.Format("<a href=\"http://twitter.com/search?q={0}\">#{1}</a>", HttpUtility.UrlEncode("#" + hashTag), hashTag));
					}
				}
			}

			return result;
		}

		#endregion
	}
}