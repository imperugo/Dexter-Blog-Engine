#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterBackofficeModelBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/19
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Web.Core.Models
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Dynamic;

	using Dexter.Shared.Dto;
	using Dexter.Navigation.Contracts;

	public class DexterBackofficeModelBase : DynamicObject
	{
		#region Fields

		private readonly ConcurrentDictionary<string, object> customProperties = new ConcurrentDictionary<string, object>();

		#endregion

		#region Public Properties

		public virtual BlogConfigurationDto ConfigurationDto { get; set; }

		public virtual IList<Feedback> Feedbacks { get; set; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Provides the implementation for operations that get member values. Classes derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
		/// </summary>
		/// <param name = "binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name = "result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name = "result" />.</param>
		/// <returns>
		/// 	true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
		/// </returns>
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (this.customProperties.ContainsKey(binder.Name))
			{
				result = this.customProperties[binder.Name];
				return true;
			}

			result = binder.ReturnType.IsValueType
				         ? Activator.CreateInstance(binder.ReturnType)
				         : null;

			return this.customProperties.TryAdd(binder.Name, result);
		}

		/// <summary>
		/// 	Provides the implementation for operations that set member values. Classes derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
		/// </summary>
		/// <param name = "binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name = "value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class, the <paramref name = "value" /> is "Test".</param>
		/// <returns>
		/// 	true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
		/// </returns>
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			this.customProperties[binder.Name] = value;

			return true;
		}

		#endregion
	}

	public class Feedback
	{
		#region Public Properties

		public FeedbackType FeedbackType { get; set; }

		public string Message { get; set; }

		#endregion
	}
}