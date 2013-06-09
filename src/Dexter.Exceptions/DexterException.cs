#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterException.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/13
// Last edit:	2013/03/13
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	public class DexterException : ApplicationException
	{
		#region Constructors and Destructors

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

		#endregion
	}
}