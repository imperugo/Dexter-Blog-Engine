#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterContanerExtensions.cs
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

namespace Dexter.Dependency
{
	using Quartz;

	public static class DexterContanerExtensions
	{
		#region Public Methods and Operators

		public static void RegisterCronJob<T>(this IDexterContainer container, string cronPattern) where T : class, IJob
		{
			string triggerName = string.Format("{0}_For_{1}_", cronPattern, typeof(T).Name);

			ITrigger trigger = TriggerBuilder.Create()
											 .WithCronSchedule(cronPattern)
											 .WithIdentity(triggerName)
											 .Build();

			IJobDetail job = JobBuilder.Create<T>()
									   .WithIdentity(typeof(T).Name)
									   .Build();

			Register<T>(container, job, trigger);
		}

		public static void RegisterDailyJob<T>(this IDexterContainer container, TimeOfDay timeOf) where T : class, IJob
		{
			string triggerName = string.Format("{0}_For_{1}_", timeOf, typeof(T).Name);
			ITrigger trigger = TriggerBuilder.Create()
											 .WithDailyTimeIntervalSchedule(x => x.OnEveryDay().StartingDailyAt(timeOf).WithMisfireHandlingInstructionFireAndProceed())
											 .WithIdentity(triggerName)
											 .Build();

			IJobDetail job = JobBuilder.Create<T>()
									   .WithIdentity(typeof(T).Name)
									   .Build();

			Register<T>(container, job, trigger);
		}

		public static void RegisterDaysOfTheWeekJob<T>(this IDexterContainer container, TimeOfDay timeOf) where T : class, IJob
		{
			string triggerName = string.Format("{0}_For_{1}_", timeOf, typeof(T).Name);
			ITrigger trigger = TriggerBuilder.Create()
											 .WithDailyTimeIntervalSchedule(x => x.OnDaysOfTheWeek().StartingDailyAt(timeOf).WithMisfireHandlingInstructionFireAndProceed())
											 .WithIdentity(triggerName)
											 .Build();

			IJobDetail job = JobBuilder.Create<T>()
									   .WithIdentity(typeof(T).Name)
									   .Build();

			Register<T>(container, job, trigger);
		}

		public static void RegisterHoursIntervalJob<T>(this IDexterContainer container, int hours) where T : class, IJob
		{
			RegisterMinutes<T>(container, hours * 60);
		}

		public static void RegisterMinutesIntervalJob<T>(this IDexterContainer container, int minutes) where T : class, IJob
		{
			RegisterMinutes<T>(container, minutes);
		}

		public static void RegisterSecondsIntervalJob<T>(this IDexterContainer container, int seconds) where T : class, IJob
		{
			RegisterSeconds<T>(container, seconds);
		}

		#endregion

		#region Methods

		private static void Register<T>(IDexterContainer container, IJobDetail job, ITrigger trigger) where T : class, IJob
		{
			IScheduler scheduler = container.Resolve<IScheduler>();

			TriggerState triggerExist = scheduler.GetTriggerState(trigger.Key);

			if (triggerExist == TriggerState.None && scheduler.CheckExists(job.Key) == false)
			{
				scheduler.ScheduleJob(job, trigger);
			}
			else
			{
				scheduler.RescheduleJob(trigger.Key, trigger);
			}

			container.Register<IJob, T>(LifeCycle.Transient);
		}

		private static void RegisterSeconds<T>(IDexterContainer container, int seconds) where T : class, IJob
		{
			string triggerName = string.Format("{0}_Seconds_Trigger_For_{1}", seconds, typeof(T).Name);

			ITrigger trigger = TriggerBuilder.Create()
											 .WithSimpleSchedule(x => x.WithIntervalInSeconds(seconds).WithMisfireHandlingInstructionFireNow().RepeatForever())
											 .WithIdentity(triggerName)
											 .Build();

			IJobDetail job = JobBuilder.Create<T>()
							.WithIdentity(typeof(T).Name)
							.Build();

			Register<T>(container, job, trigger);
		}

		private static void RegisterMinutes<T>(IDexterContainer container, int minutes) where T : class, IJob
		{
			string triggerName = string.Format("{0}_Minutes_Trigger_For_{1}", minutes, typeof(T).Name);

			ITrigger trigger = TriggerBuilder.Create()
											 .WithSimpleSchedule(x => x.WithIntervalInMinutes(minutes).WithMisfireHandlingInstructionFireNow().RepeatForever())
											 .WithIdentity(triggerName)
											 .Build();

			IJobDetail job = JobBuilder.Create<T>()
							.WithIdentity(typeof(T).Name)
							.Build();

			Register<T>(container, job, trigger);
		}

		#endregion
	}
}