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
		#region Fields

		private readonly int itemId;

		#endregion

		#region Constructors and Destructors

		public ItemNotFoundException(int itemId)
			: this("Unable to find the Item with the specified id", itemId)
		{
			this.itemId = itemId;
		}

		public ItemNotFoundException(string message, int itemId)
			: base(message)
		{
			this.itemId = itemId;
		}

		#endregion

		#region Public Properties

		public int ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		#endregion
	}
}