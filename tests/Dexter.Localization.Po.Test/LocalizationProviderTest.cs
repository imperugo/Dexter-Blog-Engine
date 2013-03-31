namespace Dexter.Localization.Po.Test
{
	using System;
	using System.IO;

	using Dexter.Caching;
	using Dexter.Localization.Exception;

	using Gaia.Localization.Po.Tests;

	using Moq;

	using Ploeh.AutoFixture;
	using Ploeh.AutoFixture.AutoMoq;

	using SharpTestsEx;

	using Xunit;

	public class LocalizationProviderTest
	{
		#region Fields

		private readonly LocalizationProvider sut;

		#endregion

		#region Constructors and Destructors

		public LocalizationProviderTest()
		{
			IFixture fixture = new Fixture().Customize(new AutoMoqCustomization());

			Mock<ICacheProvider> stubCache = new Mock<ICacheProvider>();
			stubCache.Setup(x => x.Get(It.IsAny<string>())).Returns(null);

			fixture.Register(() => stubCache.Object);

			this.sut = fixture.Create<LocalizationProvider>();
		}

		#endregion

		#region Public Methods and Operators

		[Fact]
		public void DeleteModule_WithEmptyCultureName_ShouldThrowArgumentException()
		{
			Executing.This(() => this.sut.DeleteModule(string.Empty, "Module1"))
			         .Should()
			         .Throw<ArgumentException>();
		}

		[Fact]
		public void DeleteModule_WithNotExistingModule_ShouldThrowLocalizationModuleNotFoundException()
		{
			Executing.This(() => this.sut.DeleteModule("en-US", "Module1"))
			         .Should()
			         .Throw<LocalizationModuleNotFoundException>();
		}

		[Fact]
		public void DeleteModule_WithNullCultureName_ShouldThrowArgumentNullException()
		{
			Executing.This(() => this.sut.DeleteModule(null, "Module1"))
			         .Should()
			         .Throw<ArgumentNullException>();
		}

		[Fact]
		public void GetLocalizedStringTest_WithEmptyCulture_ShouldThrowArgumentException()
		{
			Executing.This(() => this.sut.GetLocalizedString("myModule", "myText", string.Empty))
			         .Should()
			         .Throw<ArgumentException>();
		}

		[Fact]
		public void GetLocalizedStringTest_WithEmptyText_ShouldThrowArgumentException()
		{
			Executing.This(() =>
			               this.sut.GetLocalizedString("myModule", string.Empty, "en-US"))
			         .Should()
			         .Throw<ArgumentException>();
		}

		[Fact]
		public void GetLocalizedStringTest_WithNullCulture_ShouldThrowArgumentNullException()
		{
			Executing.This(() => this.sut.GetLocalizedString("myModule", "myText", null))
			         .Should()
			         .Throw<ArgumentNullException>();
		}

		[Fact]
		public void GetLocalizedStringTest_WithNullText_ShouldThrowArgumentNullException()
		{
			Executing.This(() => this.sut.GetLocalizedString("myModule", null, "en-US"))
			         .Should()
			         .Throw<ArgumentNullException>();
		}

		[Fact]
		public void GetLocalizedStringTest_WithValidDataAndWithoutScope_ShouldNotThrowException()
		{
			LocalizedString result = this.sut.GetLocalizedString("TestModule", "My message Id without scope", "en-US");

			result.Should().Not.Be.Null();
			result.Text.Should().Be.EqualTo("My message string without scope");
		}

		[Fact]
		public void GetLocalizedStringTest_WithValidData_ShouldNotThrowException()
		{
			LocalizedString result = this.sut.GetLocalizedString("TestModule", "My message Id", "en-US");

			result.Should().Not.Be.Null();
			result.Text.Should().Be.EqualTo("My message string");
		}

		[Fact]
		public void SaveModule_WithAnExistingModule_ShouldThrowLocalizationModuleExistentException()
		{
			using (Stream readStream = Helper.GetTestModuleStream())
			{
				Executing.This(() => this.sut.SaveModule("en-US", "TestModule", readStream))
				         .Should()
				         .Throw<LocalizationModuleExistentException>();
			}
		}

		[Fact]
		public void SaveModule_WithEmptyCultureName_ShouldThrowArgumentException()
		{
			using (Stream readStream = Helper.GetTestModuleStream())
			{
				Executing.This(() => this.sut.SaveModule(string.Empty, "Module1", readStream))
				         .Should()
				         .Throw<ArgumentException>();
			}
		}

		[Fact]
		public void SaveModule_WithEmptyModuleName_ShouldThrowArgumentException()
		{
			using (Stream readStream = Helper.GetTestModuleStream())
			{
				Executing.This(() => this.sut.SaveModule("en-US", string.Empty, readStream))
				         .Should()
				         .Throw<ArgumentException>();
			}
		}

		[Fact]
		public void SaveModule_WithNullCultureName_ShouldThrowArgumentNullException()
		{
			using (Stream readStream = Helper.GetTestModuleStream())
			{
				Executing.This(() => this.sut.SaveModule(null, "Module1", readStream))
				         .Should()
				         .Throw<ArgumentNullException>();
			}
		}

		[Fact]
		public void SaveModule_WithNullModuleName_ShouldThrowArgumentNullException()
		{
			using (Stream readStream = Helper.GetTestModuleStream())
			{
				Executing.This(() => this.sut.SaveModule("en-US", null, readStream))
				         .Should()
				         .Throw<ArgumentNullException>();
			}
		}

		[Fact]
		public void SaveModule_WithNullStream_ShouldThrowArgumentNullException()
		{
			Executing.This(() => this.sut.SaveModule("en-US", "Module1", null))
			         .Should()
			         .Throw<ArgumentNullException>();
		}

		[Fact]
		public void SaveOrUpdateModule_WithEmptyCultureName_ShouldThrowArgumentException()
		{
			using (Stream readStream = Helper.GetTestModuleStream())
			{
				Executing.This(() => this.sut.SaveOrUpdateModule(string.Empty, "Module1", readStream))
				         .Should()
				         .Throw<ArgumentException>();
			}
		}

		[Fact]
		public void SaveOrUpdateModule_WithEmptyModuleName_ShouldThrowArgumentException()
		{
			using (Stream readStream = Helper.GetTestModuleStream())
			{
				Executing.This(() => this.sut.SaveOrUpdateModule("en-US", string.Empty, readStream))
				         .Should()
				         .Throw<ArgumentException>();
			}
		}

		[Fact]
		public void SaveOrUpdateModule_WithNullCultureName_ShouldThrowArgumentNullException()
		{
			using (Stream readStream = Helper.GetTestModuleStream())
			{
				Executing.This(() => this.sut.SaveOrUpdateModule(null, "Module1", readStream))
				         .Should()
				         .Throw<ArgumentNullException>();
			}
		}

		[Fact]
		public void SaveOrUpdateModule_WithNullModuleName_ShouldThrowArgumentNullException()
		{
			using (Stream readStream = Helper.GetTestModuleStream())
			{
				Executing.This(() => this.sut.SaveOrUpdateModule("en-US", null, readStream))
				         .Should()
				         .Throw<ArgumentNullException>();
			}
		}

		[Fact]
		public void SaveOrUpdateModule_WithNullStream_ShouldThrowArgumentNullException()
		{
			Executing.This(() => this.sut.SaveOrUpdateModule("en-US", "Module1", null))
			         .Should()
			         .Throw<ArgumentNullException>();
		}

		#endregion
	}
}