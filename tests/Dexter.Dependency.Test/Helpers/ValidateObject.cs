#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ValidateObject.cs
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

namespace Dexter.Dependency.Test.Helpers
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class ValidateObject
	{
		#region Public Properties

		[Required(AllowEmptyStrings = false)]
		public string Value1 { get; set; }

		[Required]
		public int? Value2 { get; set; }

		[Required(AllowEmptyStrings = false)]
		public DateTimeOffset Value3 { get; set; }

		#endregion
	}
}