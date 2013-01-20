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
	using Dexter.Dependency;

	using global::Quartz;

	using global::Quartz.Spi;

	public class DexterJobFactory : IJobFactory
	{
		#region Fields

		private readonly IDexterContainer container;

		#endregion

		#region Constructors and Destructors

		public DexterJobFactory(IDexterContainer container)
		{
			this.container = container;
		}

		#endregion

		#region Public Methods and Operators

		public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
		{
			return (IJob)this.container.Resolve(bundle.JobDetail.JobType);
		}

		public void ReturnJob(IJob job)
		{
			this.container.Release(job);
		}

		#endregion
	}
}