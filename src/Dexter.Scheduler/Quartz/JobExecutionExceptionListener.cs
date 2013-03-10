namespace Dexter.Scheduler.Quartz
{
	using Common.Logging;

	using Dexter.Async;

	using global::Quartz;
	using global::Quartz.Listener;

	public class JobExecutionExceptionListener : JobListenerSupport
	{
		#region Fields

		private readonly IDexterCall bmwCall;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		public JobExecutionExceptionListener(IDexterCall bmwCall, ILog logger)
		{
			this.bmwCall = bmwCall;
			this.logger = logger;
		}

		#endregion

		#region Public Properties

		public override string Name
		{
			get
			{
				return "JobExecutionExceptionListener";
			}
		}

		#endregion

		#region Public Methods and Operators

		public override void JobToBeExecuted(IJobExecutionContext context)
		{
			this.bmwCall.StartSession();
			base.JobToBeExecuted(context);
		}

		public override void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
		{
			base.JobWasExecuted(context, jobException);

			if (jobException != null)
			{
				this.logger.Error(jobException.Message, jobException);
			}

			this.bmwCall.Complete(jobException == null);
		}

		#endregion
	}
}
