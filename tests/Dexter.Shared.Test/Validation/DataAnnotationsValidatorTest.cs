#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DataAnnotationsValidatorTest.cs
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

namespace Dexter.Shared.Test.Validation
{
	using System;

	using Dexter.Shared.Exceptions;
	using Dexter.Shared.Test.Helpers;
	using Dexter.Shared.Validation;

	using FizzWare.NBuilder;

	using Ploeh.AutoFixture;
	using Ploeh.AutoFixture.AutoMoq;

	using Xunit;

	public class DataAnnotationsValidatorTest : IDisposable
	{
		#region Fields

		private DataAnnotationsValidator sut;

		#endregion

		#region Constructors and Destructors

		public DataAnnotationsValidatorTest()
		{
			IFixture fixture = new Fixture().Customize(new AutoMoqCustomization());

			this.sut = fixture.Create<DataAnnotationsValidator>();
		}

		#endregion

		#region Public Methods and Operators

		public void Dispose()
		{
			this.sut = null;
		}

		[Fact]
		public void Validate_WithNotValidaValues_ShouldThrowThumbValidationException()
		{
			ValidateObject obj = Builder<ValidateObject>
				.CreateNew()
				.With(x => x.Value1 = null)
				.Build();

			Assert.Throws<DexterValidationException>(() => this.sut.Validate(obj));
		}

		#endregion
	}
}