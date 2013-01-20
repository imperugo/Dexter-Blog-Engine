#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			BackgroundTask.cs
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

	using Common.Logging;

	public abstract class BackgroundTask
	{
		#region Fields

		private readonly IDexterCall dexterCall;

		private readonly ILog logger;

		private bool isFault;

		#endregion

		#region Constructors and Destructors

		protected BackgroundTask(ILog logger, IDexterCall dexterCall)
		{
			this.logger = logger;
			this.dexterCall = dexterCall;
		}

		#endregion

		#region Public Methods and Operators

		public abstract void Execute();

		public void Run()
		{
			try
			{
				this.Execute();
			}
			catch (Exception e)
			{
				this.logger.Error("Could not execute task " + this.GetType().Name, e);
				this.OnError(e);
				this.isFault = true;
			}
		}

		#endregion

		#region Methods

		protected virtual void Finalize()
		{
			this.dexterCall.Complete(!this.isFault);
		}

		protected virtual void Initialize()
		{
			this.dexterCall.StartSession();
		}

		protected virtual void OnError(Exception e)
		{
		}

		#endregion
	}
}