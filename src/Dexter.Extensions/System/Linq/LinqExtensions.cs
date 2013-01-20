#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LinqExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/20
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace System.Linq
{
	using System.Collections.Generic;

	public static class LinqExtensions
	{
		#region Public Methods and Operators

		public static T GetRandom<T>(this IEnumerable<T> list)
		{
			return list.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
		}

		#endregion
	}
}