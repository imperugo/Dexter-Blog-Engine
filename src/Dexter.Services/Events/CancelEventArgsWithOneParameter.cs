#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CancelEventArgsWithOneParameter.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Events
{
	using System.ComponentModel;

	public class CancelEventArgsWithOneParameter<T, TK> : CancelEventArgs
	{
		#region Constructors and Destructors

		public CancelEventArgsWithOneParameter(T parameter, TK result)
		{
			this.Parameter = parameter;
			this.Result = result;
		}

		#endregion

		#region Public Properties

		public T Parameter { get; set; }

		public TK Result { get; set; }

		#endregion
	}
}