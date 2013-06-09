#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterRestartRequiredException.cs
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

	public class DexterRestartRequiredException : DexterException
	{
		#region Fields

		private readonly Type caller;

		#endregion

		#region Constructors and Destructors

		public DexterRestartRequiredException(Type caller)
		{
			if (caller == null)
			{
				throw new ArgumentNullException("caller");
			}

			this.caller = caller;
		}

		public DexterRestartRequiredException(string message, Type caller)
			: base(message)
		{
			if (caller == null)
			{
				throw new ArgumentNullException("caller");
			}

			this.caller = caller;
		}

		#endregion

		#region Public Properties

		public Type Caller
		{
			get
			{
				return this.caller;
			}
		}

		#endregion
	}
}