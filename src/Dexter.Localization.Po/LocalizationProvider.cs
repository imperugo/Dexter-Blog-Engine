#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LocalizationProvider.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/27
// Last edit:	2013/03/17
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Localization.Po
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Text;
	using System.Threading;

	using Common.Logging;

	using Dexter.Caching;
	using Dexter.Localization.Exception;

	public class LocalizationProvider : ILocalizationProvider
	{
		#region Static Fields

		private static readonly Dictionary<char, char> escapeTranslations = new Dictionary<char, char>
			                                                                    {
				                                                                    { 'n', '\n' }, 
				                                                                    { 'r', '\r' }, 
				                                                                    { 't', '\t' }
			                                                                    };

		private static readonly string modulesLocalizationFilePathFormat = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Localization/{0}/{1}.po");

		#endregion

		#region Fields

		private readonly ICacheProvider cacheProvider;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		/// <param name="cacheProvider">The cache provider.</param>
		/// <param name="logger">The logger.</param>
		public LocalizationProvider(ICacheProvider cacheProvider, ILog logger)
		{
			this.logger = logger;
			this.cacheProvider = cacheProvider;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Deletes the module.
		/// </summary>
		/// <param name = "cultureName">Name of the culture.</param>
		/// <param name = "moduleName">Name of the module.</param>
		/// <exception cref = "ArgumentException">Will be throw if there is an existing module with the same specified name for the same specified culture.</exception>
		/// <exception cref = "ArgumentNullException">Will be throw if <paramref name = "moduleName" /> is null.</exception>
		/// <exception cref = "ArgumentException">Will be throw if <paramref name = "moduleName" /> is empty.</exception>
		/// <exception cref = "ArgumentNullException">Will be throw if <paramref name = "cultureName" /> is null.</exception>
		/// <exception cref = "ArgumentException">Will be throw if <paramref name = "cultureName" /> is empty.</exception>
		/// <exception cref = "LocalizationModuleNotFoundException">Will be throw if there isn't an existing module with the same specified name for the same specified culture.</exception>
		public void DeleteModule(string cultureName, string moduleName)
		{
			if (moduleName == string.Empty)
			{
				throw new ArgumentException();
			}

			if (moduleName == null)
			{
				throw new ArgumentNullException();
			}

			if (cultureName == string.Empty)
			{
				throw new ArgumentException();
			}

			if (cultureName == null)
			{
				throw new ArgumentNullException();
			}

			string filePath = string.Format(modulesLocalizationFilePathFormat, cultureName, moduleName);

			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			else
			{
				throw new LocalizationModuleNotFoundException("Unable to find the specified localization module");
			}
		}

		public LocalizedString GetLocalizedString(string msgId)
		{
			return this.GetLocalizedString(msgId, Thread.CurrentThread.CurrentCulture.Name);
		}

		/// <summary>
		/// Gets the localized string.
		/// </summary>
		/// <param name="msgId">The MSG id.</param>
		/// <param name="cultureName">Name of the culture.</param>
		/// <returns>The result will never be null.</returns>
		/// <exception cref="ArgumentNullException">Will be throw if <paramref name="msgId"/> is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if <paramref name="msgId"/> is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if <paramref name="cultureName"/> is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if <paramref name="cultureName"/> is empty.</exception>
		public LocalizedString GetLocalizedString(string msgId, string cultureName)
		{
			return this.GetLocalizedString(null, msgId, cultureName);
		}

		/// <summary>
		/// Gets the localized string.
		/// </summary>
		/// <param name="moduleName">Name of the module.</param>
		/// <param name="msgId">The MSG id.</param>
		/// <param name="cultureName">Name of the culture.</param>
		/// <param name="args">The args.</param>
		/// <returns>The result will never be null.</returns>
		/// <exception cref="ArgumentNullException">Will be throw if <paramref name="msgId"/> is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if <paramref name="msgId"/> is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if <paramref name="cultureName"/> is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if <paramref name="cultureName"/> is empty.</exception>
		public LocalizedString GetLocalizedString(string moduleName, string msgId, string cultureName, params object[] args)
		{
			if (msgId == string.Empty)
			{
				throw new ArgumentException();
			}

			if (msgId == null)
			{
				throw new ArgumentNullException();
			}

			if (cultureName == string.Empty)
			{
				throw new ArgumentException();
			}

			if (cultureName == null)
			{
				throw new ArgumentNullException();
			}

			if (string.IsNullOrEmpty(moduleName))
			{
				moduleName = "core";
			}

			CultureInfo culture = new CultureInfo(cultureName);

			CultureDictionary cultureDictionary = this.LoadCulture(moduleName, culture);

			if (cultureDictionary.Translations == null)
			{
				return new LocalizedString(msgId, msgId, args);
			}

			string genericKey = ("|" + msgId).ToLowerInvariant();
			if (cultureDictionary.Translations.ContainsKey(genericKey))
			{
				LocalizedString r = new LocalizedString(cultureDictionary.Translations[genericKey], msgId, args);

				return r;
			}

			this.logger.WarnFormat("There is no value with the specified msgIf:'{0}'", msgId);

			return new LocalizedString(msgId, msgId, args);
		}

		/// <summary>
		/// Saves the module.
		/// </summary>
		/// <param name="cultureName">Name of the culture.</param>
		/// <param name="moduleName">Name of the module.</param>
		/// <param name="inputStream">The input stream.</param>
		/// <exception cref="LocalizationModuleExistentException">Will be throw if there is an existing module with the same specified name for the same specified culture.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if <paramref name="moduleName"/> is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if <paramref name="moduleName"/> is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if <paramref name="cultureName"/> is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if <paramref name="cultureName"/> is empty.</exception>
		public void SaveModule(string cultureName, string moduleName, Stream inputStream)
		{
			this.Save(moduleName, cultureName, inputStream, true);
		}

		/// <summary>
		/// Saves the or update module.
		/// </summary>
		/// <param name="cultureName">Name of the culture.</param>
		/// <param name="moduleName">Name of the module.</param>
		/// <param name="inputStream">The input stream.</param>
		/// <exception cref="ArgumentNullException">Will be throw if <paramref name="moduleName"/> is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if <paramref name="moduleName"/> is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if <paramref name="cultureName"/> is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if <paramref name="cultureName"/> is empty.</exception>
		public void SaveOrUpdateModule(string cultureName, string moduleName, Stream inputStream)
		{
			this.Save(moduleName, cultureName, inputStream, false);
		}

		#endregion

		#region Methods

		private static string ParseId(string fileLine)
		{
			return Unescape(fileLine.Substring(5).Trim().Trim('"'));
		}

		private static void ParseLocalizationStream(string text, IDictionary<string, string> translations)
		{
			StringReader reader = new StringReader(text);
			string fileLine, scope;

			string id = scope = String.Empty;

			while ((fileLine = reader.ReadLine()) != null)
			{
				if (fileLine.StartsWith("#:"))
				{
					scope = ParseScope(fileLine);
					continue;
				}

				if (fileLine.StartsWith("msgid"))
				{
					id = ParseId(fileLine);
					continue;
				}

				if (fileLine.StartsWith("msgstr"))
				{
					string translation = ParseTranslation(fileLine);

					// ignore incomplete localizations (empty msgid or msgstr)
					if (!String.IsNullOrWhiteSpace(id) && !String.IsNullOrWhiteSpace(translation))
					{
						string scopedKey = (scope + "|" + id).ToLowerInvariant();
						translations.Add(scopedKey, translation);
					}
					id = scope = String.Empty;
				}
			}
		}

		private static string ParseScope(string fileLine)
		{
			return Unescape(fileLine.Substring(2).Trim().Trim('"'));
		}

		private static string ParseTranslation(string fileLine)
		{
			return Unescape(fileLine.Substring(6).Trim().Trim('"'));
		}

		private static string Unescape(string str)
		{
			StringBuilder sb = null;
			bool escaped = false;
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (escaped)
				{
					if (sb == null)
					{
						sb = new StringBuilder(str.Length);
						if (i > 1)
						{
							sb.Append(str.Substring(0, i - 1));
						}
					}
					char unescaped;
					sb.Append(escapeTranslations.TryGetValue(c, out unescaped)
						          ? unescaped
						          : c);
					escaped = false;
				}
				else
				{
					if (c == '\\')
					{
						escaped = true;
					}
					else if (sb != null)
					{
						sb.Append(c);
					}
				}
			}
			return sb == null
				       ? str
				       : sb.ToString();
		}

		private CultureDictionary LoadCulture(string moduleName, CultureInfo culture)
		{
			string key = string.Format("Dictionary.Culture:{0}.Module{1}", culture.Name, moduleName);

			CultureDictionary cachedObject = this.cacheProvider.Get<CultureDictionary>(key);

			if (cachedObject == null)
			{
				cachedObject = new CultureDictionary
					               {
						               Culture = culture, 
						               Translations = this.LoadTranslations(moduleName, culture)
					               };
				this.cacheProvider.Put(key, cachedObject, TimeSpan.FromDays(1));
			}

			return cachedObject;
		}

		private IDictionary<string, string> LoadTranslations(string moduleName, CultureInfo culture)
		{
			string filePath = string.Format(modulesLocalizationFilePathFormat, culture.Name, moduleName);

			using (StreamReader sr = new StreamReader(filePath))
			{
				string container = sr.ReadToEnd();

				IDictionary<string, string> result = new Dictionary<string, string>();

				ParseLocalizationStream(container, result);
				return result;
			}
		}

		private void Save(string moduleName, string cultureName, Stream inputStream, bool throwExceptionIfFileExist)
		{
			if (moduleName == string.Empty)
			{
				throw new ArgumentException();
			}

			if (moduleName == null)
			{
				throw new ArgumentNullException();
			}

			if (cultureName == string.Empty)
			{
				throw new ArgumentException();
			}

			if (cultureName == null)
			{
				throw new ArgumentNullException();
			}

			if (inputStream == null)
			{
				throw new ArgumentNullException();
			}

			string filePath = string.Format(modulesLocalizationFilePathFormat, cultureName, moduleName);

			if (File.Exists(filePath))
			{
				if (throwExceptionIfFileExist)
				{
					throw new LocalizationModuleExistentException("Another localization module with the same name and the same culture exists into the repository.");
				}
			}

			using (FileStream sw = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite))
			{
				inputStream.CopyTo(sw);
			}
		}

		#endregion
	}
}