#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CancelEventArgsWithOneParameter.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
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