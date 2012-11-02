namespace Dexter.Async
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Th contract for the base call context
	/// </summary>
	public interface ICallContext
	{
		/// <summary>
		/// 	Gets the items.
		/// </summary>
		/// <value>The items.</value>
		object this[string key] { get; set; }
	}
}
