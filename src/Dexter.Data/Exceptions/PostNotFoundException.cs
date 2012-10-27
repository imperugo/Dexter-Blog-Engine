#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostNotFoundException.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
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

	public class PostNotFoundException : ArgumentException
	{
		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.ArgumentException" /> class.
		/// </summary>
		public PostNotFoundException()
		{
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message.
		/// </summary>
		/// <param name="message"> The error message that explains the reason for the exception. </param>
		public PostNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message"> The error message that explains the reason for the exception. </param>
		/// <param name="innerException"> The exception that is the cause of the current exception. If the <paramref
		/// 	 name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		public PostNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message, the parameter name, and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message"> The error message that explains the reason for the exception. </param>
		/// <param name="paramName"> The name of the parameter that caused the current exception. </param>
		/// <param name="innerException"> The exception that is the cause of the current exception. If the <paramref
		/// 	 name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		public PostNotFoundException(string message, string paramName, Exception innerException)
			: base(message, paramName, innerException)
		{
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and the name of the parameter that causes this exception.
		/// </summary>
		/// <param name="message"> The error message that explains the reason for the exception. </param>
		/// <param name="paramName"> The name of the parameter that caused the current exception. </param>
		public PostNotFoundException(string message, string paramName)
			: base(message, paramName)
		{
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with serialized data.
		/// </summary>
		/// <param name="info"> The object that holds the serialized object data. </param>
		/// <param name="context"> The contextual information about the source or destination. </param>
		protected PostNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#endregion
	}
}