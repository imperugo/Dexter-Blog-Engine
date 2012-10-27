#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LoggingExtensions.cs
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

namespace Dexter.Extensions.Logging
{
	using System;
	using System.Threading.Tasks;

	using Common.Logging;

	public static class LoggingExtensions
	{
		#region Public Methods and Operators

		public static Task DebugAsync(this ILog logger, object message)
		{
			return Task.Run(() => logger.Debug(message));
		}

		public static Task DebugAsync(this ILog logger, object message, Exception exception)
		{
			return Task.Run(() => logger.Debug(message, exception));
		}

		public static Task DebugAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Debug(formatMessageCallback));
		}

		public static Task DebugAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Debug(formatMessageCallback, exception));
		}

		public static Task DebugAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Debug(formatProvider, formatMessageCallback));
		}

		public static Task DebugAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Debug(formatProvider, formatMessageCallback, exception));
		}

		public static Task DebugFormatAsync(this ILog logger, string format, params object[] args)
		{
			return Task.Run(() => logger.DebugFormat(format, args));
		}

		public static Task DebugFormatAsync(this ILog logger, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.DebugFormat(format, exception, args));
		}

		public static Task DebugFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, params object[] args)
		{
			return Task.Run(() => logger.DebugFormat(formatProvider, format, args));
		}

		public static Task DebugFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.DebugFormat(formatProvider, format, exception, args));
		}

		public static Task ErrorAsync(this ILog logger, object message)
		{
			return Task.Run(() => logger.Error(message));
		}

		public static Task ErrorAsync(this ILog logger, object message, Exception exception)
		{
			return Task.Run(() => logger.Error(message, exception));
		}

		public static Task ErrorAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Error(formatMessageCallback));
		}

		public static Task ErrorAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Error(formatMessageCallback, exception));
		}

		public static Task ErrorAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Error(formatProvider, formatMessageCallback));
		}

		public static Task ErrorAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Error(formatProvider, formatMessageCallback, exception));
		}

		public static Task ErrorFormatAsync(this ILog logger, string format, params object[] args)
		{
			return Task.Run(() => logger.ErrorFormat(format, args));
		}

		public static Task ErrorFormatAsync(this ILog logger, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.ErrorFormat(format, exception, args));
		}

		public static Task ErrorFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, params object[] args)
		{
			return Task.Run(() => logger.ErrorFormat(formatProvider, format, args));
		}

		public static Task ErrorFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.ErrorFormat(formatProvider, format, exception, args));
		}

		public static Task FatalAsync(this ILog logger, object message)
		{
			return Task.Run(() => logger.Fatal(message));
		}

		public static Task FatalAsync(this ILog logger, object message, Exception exception)
		{
			return Task.Run(() => logger.Fatal(message, exception));
		}

		public static Task FatalAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Fatal(formatMessageCallback));
		}

		public static Task FatalAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Fatal(formatMessageCallback, exception));
		}

		public static Task FatalAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Fatal(formatProvider, formatMessageCallback));
		}

		public static Task FatalAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Fatal(formatProvider, formatMessageCallback, exception));
		}

		public static Task FatalFormatAsync(this ILog logger, string format, params object[] args)
		{
			return Task.Run(() => logger.FatalFormat(format, args));
		}

		public static Task FatalFormatAsync(this ILog logger, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.FatalFormat(format, exception, args));
		}

		public static Task FatalFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, params object[] args)
		{
			return Task.Run(() => logger.FatalFormat(formatProvider, format, args));
		}

		public static Task FatalFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.FatalFormat(formatProvider, format, exception, args));
		}

		public static Task InfoAsync(this ILog logger, object message)
		{
			return Task.Run(() => logger.Info(message));
		}

		public static Task InfoAsync(this ILog logger, object message, Exception exception)
		{
			return Task.Run(() => logger.Info(message, exception));
		}

		public static Task InfoAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Info(formatMessageCallback));
		}

		public static Task InfoAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Info(formatMessageCallback, exception));
		}

		public static Task InfoAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Info(formatProvider, formatMessageCallback));
		}

		public static Task InfoAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Info(formatProvider, formatMessageCallback, exception));
		}

		public static Task InfoFormatAsync(this ILog logger, string format, params object[] args)
		{
			return Task.Run(() => logger.InfoFormat(format, args));
		}

		public static Task InfoFormatAsync(this ILog logger, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.InfoFormat(format, exception, args));
		}

		public static Task InfoFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, params object[] args)
		{
			return Task.Run(() => logger.InfoFormat(formatProvider, format, args));
		}

		public static Task InfoFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.InfoFormat(formatProvider, format, exception, args));
		}

		public static Task TraceAsync(this ILog logger, object message)
		{
			return Task.Run(() => logger.Trace(message));
		}

		public static Task TraceAsync(this ILog logger, object message, Exception exception)
		{
			return Task.Run(() => logger.Trace(message, exception));
		}

		public static Task TraceAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Trace(formatMessageCallback));
		}

		public static Task TraceAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Trace(formatMessageCallback, exception));
		}

		public static Task TraceAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Trace(formatProvider, formatMessageCallback));
		}

		public static Task TraceAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Trace(formatProvider, formatMessageCallback, exception));
		}

		public static Task TraceFormatAsync(this ILog logger, string format, params object[] args)
		{
			return Task.Run(() => logger.TraceFormat(format, args));
		}

		public static Task TraceFormatAsync(this ILog logger, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.TraceFormat(format, exception, args));
		}

		public static Task TraceFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, params object[] args)
		{
			return Task.Run(() => logger.TraceFormat(formatProvider, format, args));
		}

		public static Task TraceFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.TraceFormat(formatProvider, format, exception, args));
		}

		public static Task WarnAsync(this ILog logger, object message)
		{
			return Task.Run(() => logger.Warn(message));
		}

		public static Task WarnAsync(this ILog logger, object message, Exception exception)
		{
			return Task.Run(() => logger.Warn(message, exception));
		}

		public static Task WarnAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Warn(formatMessageCallback));
		}

		public static Task WarnAsync(this ILog logger, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Warn(formatMessageCallback, exception));
		}

		public static Task WarnAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			return Task.Run(() => logger.Warn(formatProvider, formatMessageCallback));
		}

		public static Task WarnAsync(this ILog logger, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			return Task.Run(() => logger.Warn(formatProvider, formatMessageCallback, exception));
		}

		public static Task WarnFormatAsync(this ILog logger, string format, params object[] args)
		{
			return Task.Run(() => logger.WarnFormat(format, args));
		}

		public static Task WarnFormatAsync(this ILog logger, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.WarnFormat(format, exception, args));
		}

		public static Task WarnFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, params object[] args)
		{
			return Task.Run(() => logger.WarnFormat(formatProvider, format, args));
		}

		public static Task WarnFormatAsync(this ILog logger, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			return Task.Run(() => logger.WarnFormat(formatProvider, format, exception, args));
		}

		#endregion
	}
}