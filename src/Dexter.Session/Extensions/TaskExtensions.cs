#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TaskExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/10
// Last edit:	2013/03/10
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace System.Threading.Tasks
{
	using System;

	using Dexter.Async;
	using Dexter.Dependency;

	public static class TaskExtensions
	{
		#region Public Methods and Operators

		public static Task StartNewDexterTask(this TaskFactory factory, Action action, TaskCreationOptions options = TaskCreationOptions.None)
		{
			return factory.StartNew(CreateNewAction(action), options);
		}

		public static Task<T> StartNewDexterTask<T>(this TaskFactory factory, Func<T> action, TaskCreationOptions options = TaskCreationOptions.None)
		{
			return factory.StartNew(CreateNewFunc(action),options);
		}

		#endregion

		#region Methods

		private static Func<T> CreateNewFunc<T>(Func<T> call)
		{
			return delegate
			{
				IDexterCall dexterCall = DexterContainer.Resolve<IDexterCall>();

				dexterCall.StartSession();

				try
				{
					T returnObject = call.Invoke();
					dexterCall.Complete(true);

					return returnObject;
				}
				catch
				{
					dexterCall.Complete(false);
					throw;
				}
			};
		}

		private static Action CreateNewAction(Action call)
		{
			return delegate
				{
					IDexterCall dexterCall = DexterContainer.Resolve<IDexterCall>();

					dexterCall.StartSession();

					try
					{
						call.Invoke();
						dexterCall.Complete(true);
					}
					catch
					{
						dexterCall.Complete(false);
						throw;
					}
				};
		}

		#endregion
	}
}