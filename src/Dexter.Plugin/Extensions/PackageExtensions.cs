#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PackageExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2013/01/03
// Last edit:	2013/01/03
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Plugin.Extensions
{
	using System;
	using System.IO;

	using NuGet;

	public static class PackageExtensions
	{
		#region Constructors and Destructors

		static PackageExtensions()
		{
			string extensionFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "/App_Data/Extensions");

			ThemesFolder = new DirectoryInfo(Path.Combine(extensionFolder, "Themes"));
			PluginFolder = new DirectoryInfo(Path.Combine(extensionFolder, "Plugins"));
		}

		#endregion

		#region Public Properties

		public static DirectoryInfo PluginFolder { get; private set; }

		public static DirectoryInfo ThemesFolder { get; private set; }

		#endregion

		#region Public Methods and Operators

		public static DirectoryInfo GetPluginFolder(this IPackage package)
		{
			if (package.IsTheme())
			{
				return new DirectoryInfo(Path.Combine(ThemesFolder.FullName, package.Id));
			}

			return new DirectoryInfo(Path.Combine(PluginFolder.FullName, package.Id));
		}

		public static bool IsTheme(this IPackage package)
		{
			return package.Id.StartsWith("Dexter.Theme");
		}

		#endregion
	}
}