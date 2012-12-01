namespace Dexter.Data.Exceptions
{
	using System;

	public class CategoryNotFoundException : ArgumentException
	{
		public CategoryNotFoundException(string categoryId)
			: base("Unable to find the Category with the specified Id.", categoryId)
		{
		}

		public CategoryNotFoundException(string message, string paramName)
			: base(message, paramName)
		{
		}
	}
}