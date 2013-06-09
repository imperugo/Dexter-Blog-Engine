#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RavenJobStore.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/15
// Last edit:	2013/05/15
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Scheduler.Store
{
	using System;
	using System.Collections.Generic;

	using global::Quartz;

	using global::Quartz.Impl.Matchers;

	using global::Quartz.Spi;

	public class RavenJobStore : IJobStore
	{
		public void Initialize(ITypeLoadHelper loadHelper, ISchedulerSignaler signaler)
		{
			throw new NotImplementedException();
		}

		public void SchedulerStarted()
		{
			throw new NotImplementedException();
		}

		public void SchedulerPaused()
		{
			throw new NotImplementedException();
		}

		public void SchedulerResumed()
		{
			throw new NotImplementedException();
		}

		public void Shutdown()
		{
			throw new NotImplementedException();
		}

		public void StoreJobAndTrigger(IJobDetail newJob, IOperableTrigger newTrigger)
		{
			throw new NotImplementedException();
		}

		public bool IsJobGroupPaused(string groupName)
		{
			throw new NotImplementedException();
		}

		public bool IsTriggerGroupPaused(string groupName)
		{
			throw new NotImplementedException();
		}

		public void StoreJob(IJobDetail newJob, bool replaceExisting)
		{
			throw new NotImplementedException();
		}

		public void StoreJobsAndTriggers(IDictionary<IJobDetail, global::Quartz.Collection.ISet<ITrigger>> triggersAndJobs, bool replace)
		{
			throw new NotImplementedException();
		}

		public bool RemoveJob(JobKey jobKey)
		{
			throw new NotImplementedException();
		}

		public bool RemoveJobs(IList<JobKey> jobKeys)
		{
			throw new NotImplementedException();
		}

		public IJobDetail RetrieveJob(JobKey jobKey)
		{
			throw new NotImplementedException();
		}

		public void StoreTrigger(IOperableTrigger newTrigger, bool replaceExisting)
		{
			throw new NotImplementedException();
		}

		public bool RemoveTrigger(TriggerKey triggerKey)
		{
			throw new NotImplementedException();
		}

		public bool RemoveTriggers(IList<TriggerKey> triggerKeys)
		{
			throw new NotImplementedException();
		}

		public bool ReplaceTrigger(TriggerKey triggerKey, IOperableTrigger newTrigger)
		{
			throw new NotImplementedException();
		}

		public IOperableTrigger RetrieveTrigger(TriggerKey triggerKey)
		{
			throw new NotImplementedException();
		}

		public bool CheckExists(JobKey jobKey)
		{
			throw new NotImplementedException();
		}

		public bool CheckExists(TriggerKey triggerKey)
		{
			throw new NotImplementedException();
		}

		public void ClearAllSchedulingData()
		{
			throw new NotImplementedException();
		}

		public void StoreCalendar(string name, ICalendar calendar, bool replaceExisting, bool updateTriggers)
		{
			throw new NotImplementedException();
		}

		public bool RemoveCalendar(string calName)
		{
			throw new NotImplementedException();
		}

		public ICalendar RetrieveCalendar(string calName)
		{
			throw new NotImplementedException();
		}

		public int GetNumberOfJobs()
		{
			throw new NotImplementedException();
		}

		public int GetNumberOfTriggers()
		{
			throw new NotImplementedException();
		}

		public int GetNumberOfCalendars()
		{
			throw new NotImplementedException();
		}

		public global::Quartz.Collection.ISet<JobKey> GetJobKeys(GroupMatcher<JobKey> matcher)
		{
			throw new NotImplementedException();
		}

		public global::Quartz.Collection.ISet<TriggerKey> GetTriggerKeys(GroupMatcher<TriggerKey> matcher)
		{
			throw new NotImplementedException();
		}

		public IList<string> GetJobGroupNames()
		{
			throw new NotImplementedException();
		}

		public IList<string> GetTriggerGroupNames()
		{
			throw new NotImplementedException();
		}

		public IList<string> GetCalendarNames()
		{
			throw new NotImplementedException();
		}

		public IList<IOperableTrigger> GetTriggersForJob(JobKey jobKey)
		{
			throw new NotImplementedException();
		}

		public TriggerState GetTriggerState(TriggerKey triggerKey)
		{
			throw new NotImplementedException();
		}

		public void PauseTrigger(TriggerKey triggerKey)
		{
			throw new NotImplementedException();
		}

		public global::Quartz.Collection.ISet<string> PauseTriggers(GroupMatcher<TriggerKey> matcher)
		{
			throw new NotImplementedException();
		}

		public void PauseJob(JobKey jobKey)
		{
			throw new NotImplementedException();
		}

		public IList<string> PauseJobs(GroupMatcher<JobKey> matcher)
		{
			throw new NotImplementedException();
		}

		public void ResumeTrigger(TriggerKey triggerKey)
		{
			throw new NotImplementedException();
		}

		public IList<string> ResumeTriggers(GroupMatcher<TriggerKey> matcher)
		{
			throw new NotImplementedException();
		}

		public global::Quartz.Collection.ISet<string> GetPausedTriggerGroups()
		{
			throw new NotImplementedException();
		}

		public void ResumeJob(JobKey jobKey)
		{
			throw new NotImplementedException();
		}

		public global::Quartz.Collection.ISet<string> ResumeJobs(GroupMatcher<JobKey> matcher)
		{
			throw new NotImplementedException();
		}

		public void PauseAll()
		{
			throw new NotImplementedException();
		}

		public void ResumeAll()
		{
			throw new NotImplementedException();
		}

		public IList<IOperableTrigger> AcquireNextTriggers(DateTimeOffset noLaterThan, int maxCount, TimeSpan timeWindow)
		{
			throw new NotImplementedException();
		}

		public void ReleaseAcquiredTrigger(IOperableTrigger trigger)
		{
			throw new NotImplementedException();
		}

		public IList<TriggerFiredResult> TriggersFired(IList<IOperableTrigger> triggers)
		{
			throw new NotImplementedException();
		}

		public void TriggeredJobComplete(IOperableTrigger trigger, IJobDetail jobDetail, SchedulerInstruction triggerInstCode)
		{
			throw new NotImplementedException();
		}

		public bool SupportsPersistence { get; private set; }

		public long EstimatedTimeToReleaseAndAcquireTrigger { get; private set; }

		public bool Clustered { get; private set; }

		public string InstanceId { set; private get; }

		public string InstanceName { set; private get; }

		public int ThreadPoolSize { set; private get; }
	}
}