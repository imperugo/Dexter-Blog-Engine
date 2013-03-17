#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RavenIdResolver.cs
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

namespace Dexter.Data.Raven.AutoMapper.Resolvers
{
	using System;
	using System.Text.RegularExpressions;

	public class RavenIdResolver
	{
		#region Public Methods and Operators

		public static int Resolve(string ravenId)
		{
			Match match = Regex.Match(ravenId, @"\d+");
			string idStr = match.Value;
			int id = int.Parse(idStr);
			if (id == 0)
			{
				throw new InvalidOperationException("Id cannot be zero.");
			}
			return id;
		}

		#endregion
	}
}