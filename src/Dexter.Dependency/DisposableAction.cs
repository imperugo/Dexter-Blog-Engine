#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DisposableAction.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency
{
	using System;

	/// <summary>
	/// 	Disposabel Action Class
	/// </summary>
	public class DisposableAction : IDisposable
	{
		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="DisposableAction" /> class.
		/// </summary>
		/// <param name="action"> The action. </param>
		public DisposableAction(Action action)
		{
			this.Action = action;
		}

		#endregion

		#region Properties

		private Action Action { get; set; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Action();
		}

		#endregion
	}
}