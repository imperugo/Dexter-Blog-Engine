#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterJobFactory.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/31
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Scheduler.Quartz
{
	using System;

	using Common.Logging;

	using Dexter.Dependency;

	using global::Quartz;

	using global::Quartz.Spi;

	public class DexterJobFactory : IJobFactory
	{
		#region Fields

		private readonly IDexterContainer container;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DexterJobFactory"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="logger">The logger.</param>
		public DexterJobFactory(IDexterContainer container, ILog logger)
		{
			this.container = container;
			this.logger = logger;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Called by the scheduler at the time of the trigger firing, in order to
		/// produce a <see cref="T:Quartz.IJob" /> instance on which to call Execute.
		/// </summary>
		/// <param name="bundle">The TriggerFiredBundle from which the <see cref="T:Quartz.IJobDetail" />
		/// and other info relating to the trigger firing can be obtained.</param>
		/// <param name="scheduler">a handle to the scheduler that is about to execute the job</param>
		/// <returns>
		/// the newly instantiated Job
		/// </returns>
		/// <throws>  SchedulerException if there is a problem instantiating the Job. </throws>
		/// <remarks>
		/// It should be extremely rare for this method to throw an exception -
		/// basically only the the case where there is no way at all to instantiate
		/// and prepare the Job for execution.  When the exception is thrown, the
		/// Scheduler will move all triggers associated with the Job into the
		/// <see cref="F:Quartz.TriggerState.Error" /> state, which will require human
		/// intervention (e.g. an application restart after fixing whatever
		/// configuration problem led to the issue wih instantiating the Job.
		/// </remarks>
		public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
		{
			try
			{
				if (this.container.HasComponent(bundle.JobDetail.JobType))
				{
					this.logger.DebugFormat("Resolving component for job '{0}'.", bundle.JobDetail.JobType);
					return (IJob)this.container.Resolve(bundle.JobDetail.JobType);
				}

				this.logger.WarnFormat("No component registered for job '{0}'. Probably there are dirty data into the db", bundle.JobDetail.JobType);
				return null;
			}
			catch (Exception ex)
			{
				this.logger.FatalFormat("Unable to resolve the job '{0}'", ex, bundle.JobDetail.JobType);
				throw;
			}
		}

		/// <summary>
		/// Allows the the job factory to destroy/cleanup the job if needed.
		/// </summary>
		/// <param name="job"></param>
		public void ReturnJob(IJob job)
		{
			this.logger.DebugFormat("Releasing the component for job '{0}'.", job.GetType());
			this.container.Release(job);
		}

		#endregion
	}
}