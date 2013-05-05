#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterViewEngine.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/11
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Web.Core.ViewEngine
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Web.Mvc;

	public class DexterViewEngine : RazorViewEngine
	{
		#region Constants

		private const string CacheKeyFormat = ":ViewCacheEntry:{0}:{1}:{2}:{3}:";

		private const string CacheKeyPrefixMaster = "Master";

		private const string CacheKeyPrefixPartial = "Partial";

		private const string CacheKeyPrefixView = "View";

		#endregion

		#region Static Fields

		private static readonly string[] EmptyLocations = new string[0];

		#endregion

		#region Constructors and Destructors

		public DexterViewEngine()
		{
			this.ViewLocationFormats = new[]
				                           {
					                           "~/Extensions/Themes/ScoreMe/Frontend/Views/{1}/{0}.cshtml", 
					                           "~/Extensions/Themes/ScoreMe/Frontend/Views/{1}/{0}.cshtml", 
					                           "~/Extensions/Themes/ScoreMe/Frontend/Views/Shared/{0}.cshtml", 
					                           "~/Extensions/Themes/ScoreMe/Frontend/Views/Shared/{0}.cshtml", 
					                           "~/Extensions/Themes/ScoreMe/Frontend/Views/{1}/{0}.cshtml", 
					                           "~/Extensions/Themes/ScoreMe/Frontend/Views/{1}/{0}.cshtml", 
					                           "~/Extensions/Themes/ScoreMe/Frontend/Views/Shared/{0}.cshtml", 
					                           "~/Extensions/Themes/ScoreMe/Frontend/Views/Shared/{0}.cshtml"
				                           };
		}

		#endregion

		#region Public Methods and Operators

		public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}

			if (string.IsNullOrEmpty(viewName))
			{
				throw new ArgumentException("viewName");
			}

			string[] viewLocationsSearched;
			string[] masterLocationsSearched;

			string controllerName = controllerContext.RouteData.GetRequiredString("controller");
			string viewPath = this.GetPath(controllerContext, this.ViewLocationFormats, viewName, controllerName, CacheKeyPrefixView, useCache, out viewLocationsSearched);
			string masterPath = this.GetPath(controllerContext, this.MasterLocationFormats, masterName, controllerName, CacheKeyPrefixMaster, useCache, out masterLocationsSearched);

			if (string.IsNullOrEmpty(viewPath) || (string.IsNullOrEmpty(masterPath) && !string.IsNullOrEmpty(masterName)))
			{
				return new ViewEngineResult(viewLocationsSearched.Union(masterLocationsSearched));
			}

			return new ViewEngineResult(this.CreateView(controllerContext, viewPath, masterPath), this);
		}

		#endregion

		#region Methods

		private static bool IsSpecificPath(string name)
		{
			char c = name[0];
			return c == '~' || c == '/';
		}

		private string CreateCacheKey(string prefix, string name, string controllerName)
		{
			return string.Format(CultureInfo.InvariantCulture, CacheKeyFormat, 
				this.GetType().AssemblyQualifiedName, prefix, name, controllerName);
		}

		private string GetPath(ControllerContext controllerContext, string[] locations, string name, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
		{
			searchedLocations = EmptyLocations;

			if (string.IsNullOrEmpty(name))
			{
				return string.Empty;
			}

			if (locations == null || locations.Length == 0)
			{
				throw new InvalidOperationException();
			}

			bool nameRepresentsPath = IsSpecificPath(name);
			string cacheKey = this.CreateCacheKey(cacheKeyPrefix, name, nameRepresentsPath ? string.Empty : controllerName);

			if (useCache)
			{
				string result = this.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, cacheKey);
				if (result != null)
				{
					return result;
				}
			}

			return nameRepresentsPath ?
				       this.GetPathFromSpecificName(controllerContext, name, cacheKey, ref searchedLocations) :
				       this.GetPathFromGeneralName(controllerContext, locations, name, controllerName, cacheKey, ref searchedLocations);
		}

		private string GetPathFromGeneralName(ControllerContext controllerContext, string[] locations, string name, string controllerName, string cacheKey, ref string[] searchedLocations)
		{
			string result = string.Empty;
			searchedLocations = new string[locations.Length];

			for (int i = 0; i < locations.Length; i++)
			{
				string virtualPath = string.Format(CultureInfo.InvariantCulture, locations[i], name, controllerName);

				if (this.FileExists(controllerContext, virtualPath))
				{
					searchedLocations = EmptyLocations;
					result = virtualPath;
					this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, result);
					break;
				}

				searchedLocations[i] = virtualPath;
			}

			return result;
		}

		private string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
		{
			string result = name;

			if (!this.FileExists(controllerContext, name))
			{
				result = string.Empty;
				searchedLocations = new[] { name };
			}

			this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, result);
			return result;
		}

		#endregion
	}
}