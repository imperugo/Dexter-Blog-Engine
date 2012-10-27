namespace Dexter.Caching
{
	using System;

	/// <summary>
	/// 	Adds behaviors to the <c>Object</c> class.
	/// </summary>
	public static class ObjectExtension
	{
		#region Public Methods and Operators

		public static TInput Do<TInput>(this TInput o, Action<TInput> action) where TInput : class
		{
			if (o == null)
			{
				return null;
			}

			action(o);

			return o;
		}

		public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
		{
			if (o == null)
			{
				return null;
			}
			return evaluator(o) ? o : null;
		}

		public static TResult Return<TInput, TResult>(this TInput input, Func<TInput, TResult> evaluator, Func<TResult> defaultValueOnNullInput) where TInput : class
		{
			return input.Return(evaluator, defaultValueOnNullInput, obj => obj == null);
		}

		public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, Func<TResult> failureValue, Predicate<TInput> failureEvaluator)
		{
			if (failureEvaluator(o))
			{
				return failureValue();
			}

			return evaluator(o);
		}

		public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
		{
			if (o == null)
			{
				return null;
			}
			return evaluator(o) ? null : o;
		}

		public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator) where TResult : class where TInput : class
		{
			if (o == null)
			{
				return null;
			}

			return evaluator(o);
		}

		#endregion
	}
}