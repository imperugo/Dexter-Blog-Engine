#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterItemNotFoundException.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/14
// Last edit:	2013/03/14
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Shared.Exceptions
{
	public class DexterItemNotFoundException : DexterException
	{
		#region Fields

		private readonly int key;

		#endregion

		#region Constructors and Destructors

		public DexterItemNotFoundException(int key)
		{
			this.key = key;
		}

		public DexterItemNotFoundException(string message, int key)
			: base(message)
		{
			this.key = key;
		}

		protected DexterItemNotFoundException()
		{
		}

		protected DexterItemNotFoundException(string message)
			: base(message)
		{
		}

		#endregion

		#region Public Properties

		public int Key
		{
			get
			{
				return this.key;
			}
		}

		#endregion
	}
}