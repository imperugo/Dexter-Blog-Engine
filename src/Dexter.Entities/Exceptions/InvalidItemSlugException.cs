#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			InvalidItemSlugException.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Entities.Exceptions
{
	using System;

	/// <summary>
	/// This exception will raise when a error when an invalid slug will associated to <see cref="Item"/>.
	/// </summary>
	public class InvalidItemSlugException : SystemException
	{
		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidItemSlugException"/> class.
		/// </summary>
		public InvalidItemSlugException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidItemSlugException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public InvalidItemSlugException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidItemSlugException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerException">The inner exception.</param>
		public InvalidItemSlugException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		#endregion
	}
}