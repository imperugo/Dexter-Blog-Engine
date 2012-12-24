#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			StringExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace System
{
	#region Usings

	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;

	#endregion

	/// <summary>
	/// 	Adds behaviors to the <c>String</c> class.
	/// </summary>
	public static class StringExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Appends the specified value.
		/// </summary>
		/// <param name = "value">The value.</param>
		/// <param name = "valueToAppend">The value to append.</param>
		/// <returns></returns>
		public static string Append(this string value, string valueToAppend)
		{
			if (string.IsNullOrEmpty(value))
			{
				return valueToAppend;
			}

			if (string.IsNullOrEmpty(valueToAppend))
			{
				return value;
			}

			return Append(value, new[] { valueToAppend });
		}

		/// <summary>
		/// 	Appends the specified value.
		/// </summary>
		/// <param name = "value">The value.</param>
		/// <param name = "valuesToAppend">The values to append.</param>
		/// <returns></returns>
		public static string Append(this string value, params object[] valuesToAppend)
		{
			if (value == null && (valuesToAppend == null || valuesToAppend.Length == 0))
			{
				return null;
			}

			StringBuilder sb = new StringBuilder(value);

			foreach (object s in valuesToAppend)
			{
				if (s == null)
				{
					continue;
				}

				sb.Append(s);
			}

			return sb.ToString();
		}

		/// <summary>
		/// 	Capitalizes the specified string. (example: <c>imperugo</c> will be <c>Imperugo</c>)
		/// </summary>
		/// <param name = "value">The value.</param>
		/// <returns></returns>
		public static string Capitalize(this string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}

			return value.Substring(0, 1).ToUpper() + ((value.Length > 1)
				                                           ? value.Substring(1)
				                                           : string.Empty);
		}

		/// <summary>
		/// 	Cleans the text.
		/// </summary>
		/// <param name = "text">The text.</param>
		/// <returns></returns>
		public static string CleanHtmlText(this string text)
		{
			text = Regex.Replace(text, "<[^>]*>", string.Empty);
			text = Regex.Replace(text, "<script[^>]*>", string.Empty, RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "javascript:", string.Empty, RegexOptions.IgnoreCase);

			return text;
		}

		/// <summary>
		/// 	Cuts the specified text.
		/// </summary>
		/// <param name = "s">The text.</param>
		/// <param name = "lenght">The length.</param>
		/// <returns>The cut string</returns>
		public static string Cut(this string s, int lenght)
		{
			return Cut(s, lenght, string.Empty);
		}

		/// <summary>
		/// 	Cuts the specified text.
		/// </summary>
		/// <param name = "text">The text.</param>
		/// <param name = "lenght">The length.</param>
		/// <param name = "finalText">The final text.</param>
		/// <returns>The cut string</returns>
		public static string Cut(this string text, int lenght, string finalText)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}

			if (lenght > text.Length)
			{
				return text;
			}

			finalText = finalText ?? string.Empty;

			string returnValue = text.Substring(0, lenght);

			return string.IsNullOrEmpty(finalText)
				       ? returnValue
				       : string.Concat(returnValue, finalText);
		}

		/// <summary>
		/// 	Performs a Like compare using the specified compare pattern.
		/// </summary>
		/// <param name = "value">The source value to compare against the pattern.</param>
		/// <param name = "pattern">The pattern to use a a search pattern.</param>
		/// <returns><c>True</c> in case of successful match, otherwise <c>false</c>.</returns>
		/// <remarks>
		/// 	The default IsLike is performed using a case insensitive search.
		/// </remarks>
		public static bool IsLike(this string value, string pattern)
		{
			return IsLike(value, pattern, true);
		}

		/// <summary>
		/// 	Performs a Like compare using the specified compare patterns, using an or logic, 
		/// 	the first successful match blocks the comparison.
		/// </summary>
		/// <param name = "value">The source value to compare against the pattern.</param>
		/// <param name = "patterns">The patterns to match.</param>
		/// <returns>
		/// 	<c>True</c> in case of successful match, otherwise <c>false</c>.
		/// </returns>
		/// <remarks>
		/// 	The default IsLike is performed using a case insensitive search.
		/// </remarks>
		public static bool IsLike(this string value, params string[] patterns)
		{
			return patterns.Any(value.IsLike);
		}

		/// <summary>
		/// 	Performs a Like compare using the specified compare pattern.
		/// </summary>
		/// <param name = "value">The source value to compare against the pattern.</param>
		/// <param name = "pattern">The pattern to use a a search pattern.</param>
		/// <param name = "ignoreCase"><c>True</c> to perform a case insensitive search, otherwise <c>false</c>.</param>
		/// <returns>
		/// 	<c>True</c> in case of successful match, otherwise <c>false</c>.
		/// </returns>
		public static bool IsLike(this string value, string pattern, bool ignoreCase)
		{
			/*
			 * Se nella stringa ci sono delle '\' dobbiamo 
			 * metterci un bell'escape
			 */
			pattern = pattern.Replace(@"\", @"\\");

			/*
			 * Se nella stringa ci sono dei '.' dobbiamo 
			 * metterci un bell'escape, questa operazione
			 * � da fare dopo la precedente per evitare
			 * di raddoppiare anche queste \
			 */
			pattern = pattern.Replace(".", "\\.");

			/*
			 * Gli '*' vengono sostituiti con '.*'
			 */
			pattern = pattern.Replace("*", ".*");

			/*
			 * I '?' vengono sostituiti con il semplice '.'
			 */
			pattern = pattern.Replace("?", ".");

			/*
			 * Includiamo il nostro pattern tra
			 * \A e \z per fare in modo che matchi con
			 * l'inizio e la fine della stringa altrimenti
			 * ad es. Beatrice matcha con B*r 
			 */
			pattern = string.Concat("\\A", pattern, "\\z");

			RegexOptions options = RegexOptions.None;
			if (ignoreCase)
			{
				options |= RegexOptions.IgnoreCase;
			}

			bool match = Regex.Match(value, pattern, options).Success;

			return match;
		}

		/// <summary>
		/// 	Determines whether the specified string is null or empty.
		/// </summary>
		/// <param name = "value">The string to test.</param>
		/// <returns>
		/// 	<c>true</c> if the specified string is null or empty; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}

		/// <summary>
		/// 	Determines if the given string is a number.
		/// </summary>
		/// <param name = "value">The string to analyze.</param>
		/// <returns><c>true</c> if the given string represents a number; otherwise <c>false</c>.</returns>
		public static bool IsNumber(this string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}

			return value.All(char.IsNumber);
		}

		/// <summary>
		/// 	Lefts the specified s.
		/// </summary>
		/// <param name = "s">The s.</param>
		/// <param name = "length">The length.</param>
		/// <returns></returns>
		public static string Left(this string s, int length)
		{
			if (s == null)
			{
				return null;
			}

			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}

			if ((length == 0) || string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}

			if (length <= s.Length)
			{
				return s.Substring(0, length);
			}

			return s;
		}

		/// <summary>
		/// 	Makes the name of the valid file.
		/// </summary>
		/// <param name = "name">The name.</param>
		/// <returns></returns>
		public static string MakeValidFileName(this string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentOutOfRangeException("name", "MakeValidFileName does not work with null string");
			}

			string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
			string invalidReStr = string.Format(@"[{0}]", invalidChars);
			return Regex.Replace(name, invalidReStr, "_");
		}

		/// <summary>
		/// 	Removes the HTML comment.
		/// </summary>
		/// <param name = "text">The text.</param>
		/// <returns></returns>
		public static string RemoveHtmlComment(this string text)
		{
			return Regex.Replace(text, @"<!--((?!-->).|\r|\n)*-->", string.Empty);
		}

		/// <summary>
		/// 	Rights the specified s.
		/// </summary>
		/// <param name = "s">The s.</param>
		/// <param name = "length">The length.</param>
		/// <returns></returns>
		public static string Right(this string s, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}

			if ((length == 0) || string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}

			if (length <= s.Length)
			{
				return s.Substring(s.Length - length, length);
			}

			return s;
		}

		/// <summary>
		/// 	Splits the specified s.
		/// </summary>
		/// <param name = "s">The s.</param>
		/// <param name = "separator">The separator.</param>
		/// <returns></returns>
		public static string[] Split(this string s, string separator)
		{
			return s.Split(separator.ToCharArray());
		}

		/// <summary>
		/// 	Toes the boolean.
		/// </summary>
		/// <param name = "text">The text.</param>
		/// <returns></returns>
		public static bool ToBoolean(this string text)
		{
			return Convert.ToBoolean(text);
		}

		/// <summary>
		/// 	Toes the boolean.
		/// </summary>
		/// <param name = "text">The text.</param>
		/// <param name = "defaultValue">if set to <c>true</c> [default value].</param>
		/// <returns></returns>
		public static bool ToBoolean(this string text, bool defaultValue)
		{
			bool value;

			if (!bool.TryParse(text, out value))
			{
				return defaultValue;
			}

			return value;
		}

		/// <summary>
		/// 	Toes the date time.
		/// </summary>
		/// <param name = "s">The s.</param>
		/// <returns></returns>
		public static DateTime ToDateTime(this string s)
		{
			return Convert.ToDateTime(s);
		}

		///<summary>
		///</summary>
		///<param name = "s"></param>
		///<param name = "defaultValue"></param>
		///<returns></returns>
		public static DateTime ToDateTime(this string s, DateTime defaultValue)
		{
			DateTime v1;

			if (!DateTime.TryParse(s, out v1))
			{
				return defaultValue;
			}

			return v1;
		}

		public static float ToFloat(this string text)
		{
			return Convert.ToSingle(text);
		}

		public static float ToFloat(this string text, float defaultValue)
		{
			float value;

			if (!float.TryParse(text, out value))
			{
				return defaultValue;
			}

			return value;
		}

		public static int ToInt32(this string s)
		{
			return int.Parse(s);
		}

		public static int ToInt32(this string s, int defaultValue)
		{
			int num;

			if (!int.TryParse(s, out num))
			{
				return defaultValue;
			}

			return num;
		}

		public static long ToInt64(this string s)
		{
			return long.Parse(s);
		}

		public static long ToInt64(this string s, long defaultValue)
		{
			long num;

			if (!long.TryParse(s, out num))
			{
				return defaultValue;
			}

			return num;
		}

		#endregion
	}
}