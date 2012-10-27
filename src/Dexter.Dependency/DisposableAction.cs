namespace Dexter.Dependency
{
	using System;

	/// <summary>
	/// 	Disposabel Action Class
	/// </summary>
	public class DisposableAction : IDisposable
	{
		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="DisposableAction" /> class.
		/// </summary>
		/// <param name="action"> The action. </param>
		public DisposableAction(Action action)
		{
			this.Action = action;
		}

		#endregion

		#region Properties

		private Action Action { get; set; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Action();
		}

		#endregion
	}
}