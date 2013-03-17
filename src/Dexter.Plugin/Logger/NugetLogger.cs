#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			NugetLogger.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/03
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.PackageInstaller.Logger
{
	using Common.Logging;

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
					this.logger.DebugFormat(message, args);
					break;
				case MessageLevel.Info:
					this.logger.InfoFormat(message, args);
					break;
				case MessageLevel.Warning:
					this.logger.WarnFormat(message, args);
					break;
				case MessageLevel.Error:
					this.logger.ErrorFormat(message, args);
					break;
			}
		}

		#endregion
	}
}