namespace Dexter.Dependency.Attributes
{
	using System;

	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public class ValidateAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class EnableValidation : Attribute
	{
	}
}