#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RavenIdHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/17
// Last edit:	2013/03/18
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Helpers
{
	using System;
	using System.Linq;
	using System.Text.RegularExpressions;

	using global::Raven.Client.Util;

	public class RavenIdHelper
	{
		#region Public Methods and Operators

		public static int[] Resolve(string[] ravenId)
		{
			if (ravenId == null || !ravenId.Any())
			{
				return new int[0];
			}

			return ravenId.Select(Resolve).ToArray();
		}

		public static int Resolve(string ravenId)
		{
			if (string.IsNullOrEmpty(ravenId))
			{
				return 0;
			}

			Match match = Regex.Match(ravenId, @"\d+");
			string idStr = match.Value;
			int id = int.Parse(idStr);
			if (id == 0)
			{
				throw new InvalidOperationException("Id cannot be zero.");
			}
			return id;
		}

		public static string[] Resolve<T>(int[] ravenId)
		{
			if (ravenId == null || !ravenId.Any())
			{
				return new string[0];
			}

			return ravenId.Select(Resolve<T>).ToArray();
		}

		public static string Resolve<T>(int? id)
		{
			if (!id.HasValue)
			{
				return null;
			}

			return Resolve<T>(id.Value);
		}

		public static string Resolve<T>(int id)
		{
			if (id < 1)
			{
				return null;
			}

			var entityName = Inflector.Pluralize(typeof(T).Name).ToLowerInvariant();

			return string.Format("{0}/{1}", entityName, id);
		}

		#endregion
	}
}