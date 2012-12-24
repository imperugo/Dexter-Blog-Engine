#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ObjectExtension.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

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