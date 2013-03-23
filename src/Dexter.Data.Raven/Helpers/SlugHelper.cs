#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SlugHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/02
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Helpers
{
	using System;
	using System.Globalization;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Web;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities.Exceptions;

	internal static class SlugHelper
	{
		#region Public Methods and Operators

		public static string GenerateSlug<T>(string title, string key, Func<string, T> getbyslug) where T : EntityBase<string>
		{
			if (string.IsNullOrEmpty(title))
			{
				throw new ArgumentNullException("title");
			}

			const char WordSeparator = '-';

			string entryName = RemoveNonWordCharacters(title);
			entryName = ReplaceSpacesWithSeparator(entryName, WordSeparator);
			entryName = ReplaceUnicodeCharacters(entryName);
			entryName = HttpUtility.UrlEncode(entryName);
			entryName = RemoveTrailingPeriods(entryName);
			entryName = entryName.Trim(new[]
				                           {
					                           WordSeparator
				                           });
			entryName = RemoveDoubleCharacter(entryName, '.');
			entryName = RemoveDoubleCharacter(entryName, WordSeparator);

			if (entryName.IsNumber())
			{
				entryName = "n" + WordSeparator + entryName;
			}

			entryName = entryName.ToLower(CultureInfo.InvariantCulture);

			T element = getbyslug(entryName);
			int tryCount = 0;

			while (element != null)
			{
				if (element.Id == key)
				{
					break;
				}

				switch (tryCount)
				{
					case 0:
						entryName = string.Concat(entryName, WordSeparator, "Again");
						break;
					case 1:
						entryName = string.Concat(entryName, WordSeparator, "Yet", WordSeparator, "Again");
						break;
					case 2:
						entryName = string.Concat(entryName, WordSeparator, "And", WordSeparator, "Again");
						break;
					case 3:
						entryName = string.Concat(entryName, WordSeparator, "Once", WordSeparator, "More");
						break;
					case 4:
						entryName = string.Concat(entryName, WordSeparator, "Last", WordSeparator, "One");
						break;
					default:
						entryName = string.Concat(entryName, WordSeparator, tryCount);
						break;
				}

				if (tryCount++ > 15)
				{
					throw new InvalidItemSlugException();
				}

				element = getbyslug(entryName);
			}

			return entryName;
		}

		#endregion

		#region Methods

		private static string RemoveDoubleCharacter(string text, char character)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}

			if (character == char.MinValue)
			{
				return text;
			}

			char[] newString = new char[text.Length];
			int i = 0;

			bool lastCharIsOurChar = false;
			foreach (char c in text)
			{
				if (c != character || !lastCharIsOurChar)
				{
					newString[i] = c;
					i++;
				}

				lastCharIsOurChar = c == character;
			}

			return new string(newString, 0, i);
		}

		private static string RemoveNonWordCharacters(string text)
		{
			Regex regex = new Regex(@"[\w\d\.\- ]+", RegexOptions.Compiled);
			MatchCollection matches = regex.Matches(text);
			StringBuilder cleansedText = new StringBuilder();

			foreach (Match match in matches)
			{
				if (match.Value.Length > 0)
				{
					cleansedText.Append(match.Value);
				}
			}

			return cleansedText.ToString();
		}

		private static string RemoveTrailingPeriods(string text)
		{
			Regex regex = new Regex(@"\.+$", RegexOptions.Compiled);
			return regex.Replace(text, string.Empty);
		}

		private static string ReplaceSpacesWithSeparator(string text, char wordSeparator)
		{
			return text.Replace(' ', wordSeparator);
		}

		private static string ReplaceUnicodeCharacters(string text)
		{
			string normalized = text.Normalize(NormalizationForm.FormKD);
			Encoding removal = Encoding.GetEncoding(Encoding.ASCII.CodePage, new EncoderReplacementFallback(string.Empty), new DecoderReplacementFallback(string.Empty));
			byte[] bytes = removal.GetBytes(normalized);
			return Encoding.ASCII.GetString(bytes);
		}

		#endregion
	}
}