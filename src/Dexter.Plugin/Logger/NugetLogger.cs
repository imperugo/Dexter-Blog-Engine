#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			NugetLogger.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2013/01/03
// Last edit:	2013/01/03
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Plugin.Logger
{
	using Common.Logging;

	using Dexter.Extensions.Logging;

	using NuGet;

	public class NugetLogger : ILogger
	{
		#region Fields

		private readonly ILog logger = LogManager.GetLogger(typeof(NugetLogger));

		#endregion

		#region Public Methods and Operators

		public void Log(MessageLevel level, string message, params object[] args)
		{
			switch (level)
			{
				case MessageLevel.Debug:
					break;
				case MessageLevel.Info:
					this.logger.InfoFormatAsync(message, args);
					break;
				case MessageLevel.Warning:
					this.logger.WarnFormatAsync(message, args);
					break;
				case MessageLevel.Error:
					this.logger.ErrorFormatAsync(message, args);
					break;
			}
		}

		#endregion
	}
}