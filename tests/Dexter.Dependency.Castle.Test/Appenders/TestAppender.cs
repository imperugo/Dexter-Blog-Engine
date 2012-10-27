#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TestAppender.cs
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

namespace Dexter.Dependency.Castle.Test.Appenders
{
	using System;
	using System.Collections.Generic;

	using log4net.Appender;
	using log4net.Core;

	public class TestAppender : AppenderSkeleton
	{
		#region Static Fields

		/// <summary>
		/// 	This is the list of logs
		/// </summary>
		[ThreadStatic]
		public static List<LoggingEvent> Logs;

		#endregion

		#region Properties

		/// <summary>
		/// 	This appender requires a <see cref="log4net.Layout" /> to be set.
		/// </summary>
		/// <value> <c>true</c> </value>
		/// <remarks>
		/// 	<para> This appender requires a <see cref="log4net.Layout" /> to be set. </para>
		/// </remarks>
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// 	This method is called by the <see cref="AppenderSkeleton.DoAppend(LoggingEvent)" /> method.
		/// 	It calls static method <see cref="AppenderService.Append" /> that is used to notify the log
		/// 	to all registered clients.
		/// </summary>
		/// <param name="loggingEvent"> The event to log. </param>
		/// <remarks>
		/// 	<para> Send the event to all registered listener. </para>
		/// 	<para> Exceptions are passed to the <see cref="AppenderSkeleton.ErrorHandler" /> . </para>
		/// </remarks>
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (Logs == null)
			{
				Logs = new List<LoggingEvent>();
			}

			Logs.Add(loggingEvent);
		}

		/// <summary>
		/// 	Close the appender
		/// </summary>
		protected override void OnClose()
		{
		}

		#endregion
	}
}