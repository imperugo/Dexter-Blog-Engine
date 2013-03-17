#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SimplyCategoryValidationAttribute.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/26
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.App_Start.Validators
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	using Dexter.Host.Areas.Dxt_Admin.Models.Post;

	public class SimplyCategoryValidationAttribute : ValidationAttribute
	{
		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplyCategoryValidationAttribute"/> class.
		/// </summary>
		public SimplyCategoryValidationAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplyCategoryValidationAttribute"/> class.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		public SimplyCategoryValidationAttribute(string errorMessage)
			: base(errorMessage)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplyCategoryValidationAttribute"/> class.
		/// </summary>
		/// <param name="errorMessageAccessor">The function that enables access to validation resources.</param>
		/// <exception cref="T:System:ArgumentNullException">
		/// <paramref name="errorMessageAccessor"/> is null.</exception>
		public SimplyCategoryValidationAttribute(Func<string> errorMessageAccessor)
			: base(errorMessageAccessor)
		{
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Determines whether the specified value of the object is valid.
		/// </summary>
		/// <param name="value">The value of the object to validate.</param>
		/// <returns>
		/// true if the specified value is valid; otherwise, false.
		/// </returns>
		public override bool IsValid(object value)
		{
			if (value == null)
			{
				return false;
			}

			IEnumerable<string> categories = value as IEnumerable<string>;

			if (categories == null)
			{
				return false;
			}

			IEnumerable<string> selectedCat = categories.Where(string.IsNullOrEmpty);

			return !selectedCat.Any();
		}

		#endregion
	}
}