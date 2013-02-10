#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ILocalizationProvider.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/27
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Localization
{
	using System;
	using System.Globalization;
	using System.IO;

	using Dexter.Localization.Exception;

	public interface ILocalizationProvider
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Deletes the module.
		/// </summary>
		/// <param name="cultureName"> Name of the culture. </param>
		/// <param name="moduleName"> Name of the module. </param>
		/// <exception cref="ArgumentException">Will be throw if there is an existing module with the same specified name for the same specified culture.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="moduleName" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="moduleName" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is empty.</exception>
		/// <exception cref="LocalizationModuleNotFoundException">Will be throw if there isn't an existing module with the same specified name for the same specified culture.</exception>
		void DeleteModule(string cultureName, string moduleName);

		/// <summary>
		/// 	Gets the localized string.
		/// </summary>
		/// <param name="msgId"> The MSG id. </param>
		/// <param name="cultureName"> Name of the culture. </param>
		/// <returns> The result will never be null. </returns>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="msgId" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="msgId" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is empty.</exception>
		LocalizedString GetLocalizedString(string msgId, string cultureName);

		/// <summary>
		/// 	Gets the localized string.
		/// </summary>
		/// <param name="moduleName"> Name of the module. </param>
		/// <param name="msgId"> The MSG id. </param>
		/// <param name="cultureName"> Name of the culture. </param>
		/// <param name="args"> The args. </param>
		/// <returns> The result will never be null. </returns>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="msgId" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="msgId" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is empty.</exception>
		LocalizedString GetLocalizedString(string moduleName, string msgId, string cultureName, params object[] args);

		/// <summary>
		/// 	Saves the module.
		/// </summary>
		/// <param name="cultureName"> Name of the culture. </param>
		/// <param name="moduleName"> Name of the module. </param>
		/// <param name="inputStream"> The input stream. </param>
		/// <exception cref="LocalizationModuleExistentException">Will be throw if there is an existing module with the same specified name for the same specified culture.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="moduleName" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="moduleName" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="inputStream" />
		/// 	is null.</exception>
		void SaveModule(string cultureName, string moduleName, Stream inputStream);

		/// <summary>
		/// 	Saves the or update module.
		/// </summary>
		/// <param name="cultureName"> Name of the culture. </param>
		/// <param name="moduleName"> Name of the module. </param>
		/// <param name="inputStream"> The input stream. </param>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="moduleName" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="moduleName" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is null.</exception>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="cultureName" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="inputStream" />
		/// 	is null.</exception>
		void SaveOrUpdateModule(string cultureName, string moduleName, Stream inputStream);

		#endregion
	}
}