#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterInfo.cs
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

namespace Dexter.Entities
{
	using System;

	public class DexterInfo
	{
		#region Static Fields

		private static readonly string generator;

		private static readonly string userAgent;

		private static readonly Version version;

		private static readonly string website;

		#endregion

		#region Constructors and Destructors

		static DexterInfo()
		{
			version = typeof(DexterInfo).Assembly.GetName().Version;
			userAgent = string.Format("Dexter/{0}", version);
			website = "http://www.dexterblogengine.com";
			generator = string.Format("Dexter Blog Engine ({0}) version {1}", website, version);
		}

		#endregion

		#region Public Properties

		public static string Generator
		{
			get
			{
				return generator;
			}
		}

		public static string UserAgent
		{
			get
			{
				return userAgent;
			}
		}

		public static Version Version
		{
			get
			{
				return version;
			}
		}

		public static string Website
		{
			get
			{
				return website;
			}
		}

		#endregion
	}
}