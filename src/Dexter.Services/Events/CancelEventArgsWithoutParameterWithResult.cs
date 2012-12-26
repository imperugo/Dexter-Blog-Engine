namespace Dexter.Services.Events
{
	using System.ComponentModel;

	public class CancelEventArgsWithoutParameterWithResult<T> : CancelEventArgs
	{
		#region Constructors and Destructors

		public CancelEventArgsWithoutParameterWithResult(T result)
		{
			this.Result = result;
		}

		#endregion

		#region Public Properties

		public T Result { get; set; }

		#endregion
	}
}