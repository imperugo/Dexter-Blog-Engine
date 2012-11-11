using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dexter.Services.Implmentation.BackgroundTasks
{
	using Common.Logging;

	using Dexter.Async;
	using Dexter.Async.TaskExecutor;

	public class AddTrackbackTask : BackgroundTask
	{
		public AddTrackbackTask(ILog logger, IDexterCall dexterCall)
			: base(logger, dexterCall)
		{
		}

		public override void Execute()
		{
			throw new NotImplementedException();
		}
	}
}
