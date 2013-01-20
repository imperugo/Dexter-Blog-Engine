#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			EventHandlerExtension.cs
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

namespace System
{
	/// <summary>
	///	 Add functions to the EventHandler.
	/// </summary>
	public static class EventHandlerExtension
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Raises the specified handler.
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "handler">The handler.</param>
		/// <param name = "sender">The sender.</param>
		/// <param name = "args">The args.</param>
		public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
			where T : EventArgs
		{
			if (handler != null)
			{
				handler.Invoke(sender, args);
			}
		}

		#endregion
	}
}