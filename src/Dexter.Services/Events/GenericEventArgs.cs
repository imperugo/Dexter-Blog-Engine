#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			GenericEventArgs.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services.Events
{
	#region Usings

	using System;

	#endregion

	public class GenericEventArgs<T> : EventArgs
	{
		#region Constructors and Destructors

		public GenericEventArgs(T returnObject)
		{
			this.Objects = returnObject;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the object.
		/// </summary>
		/// <value>The object.</value>
		public T Objects { get; set; }

		#endregion
	}
}