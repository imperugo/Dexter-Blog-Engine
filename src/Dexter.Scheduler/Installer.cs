#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
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

namespace Dexter.Scheduler
{
	using System;

	using Dexter.Dependency;
	using Dexter.Dependency.Installation;
	using Dexter.Scheduler.Jobs;
	using Dexter.Scheduler.Quartz;

	using global::Quartz;

	using global::Quartz.Impl;

	using global::Quartz.Spi;

	public class Installer : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<IJobFactory, DexterJobFactory>(LifeCycle.Singleton);

			StdSchedulerFactory stdSchedulerFactory = new StdSchedulerFactory();
			IScheduler scheduler = stdSchedulerFactory.GetScheduler();
			scheduler.JobFactory = container.Resolve<IJobFactory>();

			container.Register(typeof(ISchedulerFactory), stdSchedulerFactory, LifeCycle.Singleton);

			container.Register(typeof(IScheduler), scheduler, LifeCycle.Singleton);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
			container.Resolve<IScheduler>().StartDelayed(TimeSpan.FromSeconds(10));

			container.RegisterMinutesIntervalJob<EmailNotificationJob>(5);
			container.RegisterDailyJob<PluginUpdateJob>(TimeOfDay.HourAndMinuteOfDay(01, 30));
		}

		#endregion
	}
}