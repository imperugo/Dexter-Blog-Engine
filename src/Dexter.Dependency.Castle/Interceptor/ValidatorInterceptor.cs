#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ValidatorInterceptor.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/20
// Last edit:	2013/05/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Dependency.Castle.Interceptor
{
	using System.Reflection;

	using Dexter.Dependency.Attributes;
	using Dexter.Dependency.Validator;

	using global::Castle.DynamicProxy;

	public class ValidationInterceptor : IInterceptor
	{
		#region Fields

		private readonly IObjectValidator validator;

		#endregion

		#region Constructors and Destructors

		public ValidationInterceptor()
			: this(new DataAnnotationsValidator())
		{
		}

		public ValidationInterceptor(IObjectValidator validator)
		{
			this.validator = validator;
		}

		#endregion

		#region Public Methods and Operators

		public void Intercept(IInvocation invocation)
		{
			ParameterInfo[] parameters = invocation.Method.GetParameters();
			for (int index = 0; index < parameters.Length; index++)
			{
				ParameterInfo paramInfo = parameters[index];
				object[] attributes = paramInfo.GetCustomAttributes(typeof(ValidateAttribute), false);

				if (attributes.Length == 0)
				{
					continue;
				}

				this.validator.Validate(invocation.Arguments[index]);
			}

			invocation.Proceed();
		}

		#endregion
	}
}