#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IObjectValidator.cs
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

	public interface IObjectValidator
	{
		#region Public Methods and Operators

		bool TryValidate(object @object, out ICollection<ValidationResult> results);

		void Validate(object @object);

		#endregion
	}
}