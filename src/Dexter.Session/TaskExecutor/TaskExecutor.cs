#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TaskExecutor.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/12
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Async.TaskExecutor
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	using Common.Logging;

	public class TaskExecutor : ITaskExecutor
	{
		#region Static Fields

		private static readonly ThreadLocal<List<BackgroundTask>> TasksToExecute = new ThreadLocal<List<BackgroundTask>>(() => new List<BackgroundTask>());

		#endregion

		#region Fields

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		public TaskExecutor(ILog logger)
		{
			this.logger = logger;
		}

		#endregion

		#region Public Properties

		public Action<Exception> ExceptionHandler { get; set; }

		#endregion

		#region Public Methods and Operators

		public void Discard()
		{
			TasksToExecute.Value.Clear();
			this.logger.Debug("Cleaning the Task Excecutor");
		}

		public void ExcuteLater(BackgroundTask task)
		{
			TasksToExecute.Value.Add(task);
			this.logger.DebugFormat("Adding '{0}' to the Task Executor", task);
		}

		public void StartExecuting()
		{
			logger.Debug("Starting Task Executor");

			List<BackgroundTask> value = TasksToExecute.Value;
			BackgroundTask[] copy = value.ToArray();
			value.Clear();

			if (copy.Length > 0)
			{
				Task.Factory.StartNewDexterTask(() =>
					{
						foreach (BackgroundTask backgroundTask in copy)
						{
							this.logger.DebugFormat("Running '{0}'", backgroundTask);
							backgroundTask.Run();
						}
					}, TaskCreationOptions.LongRunning)
				    .ContinueWith(task =>
					    {
						    if (this.ExceptionHandler != null)
						    {
							    logger.ErrorFormat("An error ocured running '{0}'", task, task.Exception);
							    this.ExceptionHandler(task.Exception);
						    }
					    }, TaskContinuationOptions.OnlyOnFaulted);
			}
			else
			{
				logger.Debug("Ending Task Executor");
			}
		}

		#endregion
	}
}