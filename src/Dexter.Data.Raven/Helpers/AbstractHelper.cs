#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AbstractHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/10
// Last edit:	2013/03/10
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Helpers
{
	using System;

	internal static class AbstractHelper
	{
		#region Public Methods and Operators

		public static string GenerateAbstract(string content, int lenght = 247, string separator = "...")
		{
			return content.CleanHtmlText().Cut(lenght, separator).Trim();
		}

		#endregion
	}
}