#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Log4NetMemoryAppender.cs
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
	using System.Text;

	using log4net.Appender;
	using log4net.Core;

	public class Log4NetMemoryAppender : AppenderSkeleton
	{
		#region Static Fields

		private static readonly StringBuilder Sb = new StringBuilder();

		#endregion

		#region Properties

		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		#endregion

		#region Methods

		protected override void Append(LoggingEvent loggingEvent)
		{
			Sb.Append(loggingEvent.RenderedMessage);
			if (loggingEvent.ExceptionObject != null)
			{
				Sb.AppendFormat("\n{0}", loggingEvent.ExceptionObject);
			}
		}

		#endregion
	}
}