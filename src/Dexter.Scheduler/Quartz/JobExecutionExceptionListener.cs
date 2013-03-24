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

		/// <summary>
		/// Initializes a new instance of the <see cref="JobExecutionExceptionListener"/> class.
		/// </summary>
		/// <param name="bmwCall">The BMW call.</param>
		/// <param name="logger">The logger.</param>
		public JobExecutionExceptionListener(IDexterCall bmwCall, ILog logger)
		{
			this.bmwCall = bmwCall;
			this.logger = logger;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Get the name of the <see cref="T:Quartz.IJobListener" />.
		/// </summary>
		public override string Name
		{
			get
			{
				return "JobExecutionExceptionListener";
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Called by the <see cref="T:Quartz.IScheduler" /> when a <see cref="T:Quartz.IJobDetail" />
		/// is about to be executed (an associated <see cref="T:Quartz.ITrigger" />
		/// has occured).
		/// <para>
		/// This method will not be invoked if the execution of the Job was vetoed
		/// by a <see cref="T:Quartz.ITriggerListener" />.
		/// </para>
		/// </summary>
		/// <param name="context"></param>
		/// <seealso cref="M:Quartz.Listener.JobListenerSupport.JobExecutionVetoed(Quartz.IJobExecutionContext)" />
		public override void JobToBeExecuted(IJobExecutionContext context)
		{
			this.bmwCall.StartSession();
			base.JobToBeExecuted(context);
		}

		/// <summary>
		/// Called by the <see cref="T:Quartz.IScheduler" /> after a <see cref="T:Quartz.IJobDetail" />
		/// has been executed, and be for the associated <see cref="T:Quartz.ITrigger" />'s
		/// <see cref="M:Quartz.Spi.IOperableTrigger.Triggered(Quartz.ICalendar)" /> method has been called.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="jobException"></param>
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
