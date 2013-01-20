#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/02
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Async
{
	using Dexter.Async.Async;
	using Dexter.Async.TaskExecutor;
	using Dexter.Async.Web;
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;

	public class Installer : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
			container.Register<ICallContextFactory, DexterCallContextFactory>(LifeCycle.Singleton);
			container.Register<IAsyncCallContext, AsyncCallContext>(LifeCycle.Singleton);
			container.Register<IWebCallContext, WebCallContext>(LifeCycle.Singleton);
			container.Register<ITaskExecutor, TaskExecutor.TaskExecutor>(LifeCycle.Singleton);
		}

		public void ServiceRegistration(IDexterContainer container)
		{
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
		}

		#endregion
	}
}