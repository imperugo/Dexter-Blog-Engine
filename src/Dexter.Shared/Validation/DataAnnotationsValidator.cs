#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DataAnnotationsValidator.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/09
// Last edit:	2013/05/09
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Shared.Validation
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	using Dexter.Shared.Exceptions;

	public class DataAnnotationsValidator : IObjectValidator
	{
		#region Public Methods and Operators

		/// <summary>
		///     Tries to validate the specified object using the Data Annotations
		/// </summary>
		/// <param name="object">The object.</param>
		/// <param name="results">The results.</param>
		/// <returns>
		///     <c>True</c> is the object properties are valid. Otherwise <c>False</c>.
		/// </returns>
		public bool TryValidate(object @object, out ICollection<ValidationResult> results)
		{
			ValidationContext context = new ValidationContext(@object, serviceProvider: null, items: null);
			results = new List<ValidationResult>();
			return Validator.TryValidateObject(
				@object, context, results, 
				validateAllProperties: true);
		}

		/// <summary>
		///     Validates the specified object.
		/// </summary>
		/// <param name="object">The object.</param>
		/// <exception cref="DexterValidationException">For each properties that doesn't respect the Data Annotations.</exception>
		public void Validate(object @object)
		{
			if (@object == null)
			{
				throw new DexterValidationException("The parameter must not be null.", "object");
			}

			ICollection<ValidationResult> results;

			bool isValid = this.TryValidate(@object, out results);

			if (!isValid)
			{
				ValidationResult result = results.First();

				throw new DexterValidationException(result.ErrorMessage, result.MemberNames.FirstOrDefault());
			}
		}

		#endregion
	}
}