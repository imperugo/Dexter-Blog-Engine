#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ItemNotFoundException.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Exceptions
{
	using System;

	public class ItemNotFoundException : ArgumentException
	{
		#region Constructors and Destructors

		public ItemNotFoundException(string parameterName)
			: base("Unable to find the Item with the specified filter.", parameterName)
		{
		}

		public ItemNotFoundException(string message, string paramName)
			: base(message, paramName)
		{
		}

		#endregion
	}
}