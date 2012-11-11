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