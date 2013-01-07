#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ItemNotFoundException.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Exceptions
{
	using System;
	using System.Runtime.Serialization;

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

	public class DexterException : ApplicationException
	{
		public DexterException()
		{
		}

		public DexterException(string message)
			: base(message)
		{
		}

		public DexterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected DexterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}