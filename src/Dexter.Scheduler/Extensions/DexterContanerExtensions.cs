#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterContanerExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/31
// Last edit:	2012/12/31
// License:		GNU Library General Public License (LGPL)
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

		public static void RegisterCronJob<T>(this IDexterContainer container, string cronPattern) where T : IJob
		{
			ITrigger trigger = TriggerBuilder.Create()
			                                 .WithCronSchedule(cronPattern)
			                                 .Build();

			IJobDetail job = JobBuilder.Create<T>()
			                           .Build();

			IScheduler scheduler = container.Resolve<IScheduler>();

			scheduler.ScheduleJob(job, trigger);
		}

		public static void RegisterDailyJob<T>(this IDexterContainer container, TimeOfDay timeOf) where T : IJob
		{
			ITrigger trigger = TriggerBuilder.Create()
			                                 .WithDailyTimeIntervalSchedule(x => x.OnEveryDay().StartingDailyAt(timeOf))
			                                 .Build();

			IJobDetail job = JobBuilder.Create<T>()
			                           .Build();

			IScheduler scheduler = container.Resolve<IScheduler>();

			scheduler.ScheduleJob(job, trigger);
		}

		public static void RegisterDaysOfTheWeekJob<T>(this IDexterContainer container, TimeOfDay timeOf) where T : IJob
		{
			ITrigger trigger = TriggerBuilder.Create()
			                                 .WithDailyTimeIntervalSchedule(x => x.OnDaysOfTheWeek().StartingDailyAt(timeOf))
			                                 .Build();

			IJobDetail job = JobBuilder.Create<T>()
			                           .Build();

			IScheduler scheduler = container.Resolve<IScheduler>();

			scheduler.ScheduleJob(job, trigger);
		}

		public static void RegisterHoursIntervalJob<T>(this IDexterContainer container, int hours) where T : IJob
		{
			ITrigger trigger = TriggerBuilder.Create()
			                                 .WithSimpleSchedule(x => x.WithIntervalInHours(hours))
			                                 .Build();

			IJobDetail job = JobBuilder.Create<T>()
			                           .Build();

			IScheduler scheduler = container.Resolve<IScheduler>();

			scheduler.ScheduleJob(job, trigger);
		}

		public static void RegisterMinutesIntervalJob<T>(this IDexterContainer container, int minutes) where T : IJob
		{
			ITrigger trigger = TriggerBuilder.Create()
			                                 .WithSimpleSchedule(x => x.WithIntervalInMinutes(minutes))
			                                 .Build();

			IJobDetail job = JobBuilder.Create<T>()
			                           .Build();

			IScheduler scheduler = container.Resolve<IScheduler>();

			scheduler.ScheduleJob(job, trigger);
		}

		#endregion
	}
}