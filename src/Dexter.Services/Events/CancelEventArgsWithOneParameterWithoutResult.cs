#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CancelEventArgsWithOneParameterWithoutResult.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/26
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services.Events
{
	using System.ComponentModel;

	public class CancelEventArgsWithOneParameterWithoutResult<T> : CancelEventArgs
	{
		#region Constructors and Destructors

		public CancelEventArgsWithOneParameterWithoutResult(T parameter)
		{
			this.Parameter = parameter;
		}

		#endregion

		#region Public Properties

		public T Parameter { get; set; }

		#endregion
	}
}