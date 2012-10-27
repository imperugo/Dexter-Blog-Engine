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