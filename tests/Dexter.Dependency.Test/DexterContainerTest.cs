namespace Dexter.Dependency.Test
{
	using System;

	using Moq;

	using SharpTestsEx;

	using Xunit;

	public class DexterContainerTest
	{
		#region Fields

		private readonly Mock<IDexterContainer> mockContainer;

		#endregion

		#region Constructors and Destructors

		public DexterContainerTest()
		{
			this.mockContainer = new Mock<IDexterContainer>();
		}

		#endregion

		#region Public Methods and Operators

		[Fact]
		public void Configure_ShouldCallConfigureFromMockEngine()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.Configure("myConfig");

			this.mockContainer.Verify(x => x.Configure("myConfig"), Times.Once());
		}

		[Fact]
		public void HasComponentByKey_WithValidData_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.HasComponent(typeof(DexterContainer));

			this.mockContainer.Verify(x => x.HasComponent(typeof(DexterContainer)), Times.Once());
		}

		[Fact]
		public void RegisterByGenericsByAssemblyByLifeCycleEnum_WithNullAssembly_ShouldThrowArgumentNullException()
		{
			Executing.This(() => 
							DexterContainer.Register<DexterContainerTest>(null, LifeCycle.Singleton))
								.Should()
								.Throw<ArgumentNullException>();
		}

		[Fact]
		public void RegisterByGenericsByAssemblyByLifeCycleEnum_WithValidData_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.Register<DexterContainerTest>(this.GetType().Assembly, LifeCycle.Singleton);

			this.mockContainer.Verify(x => x.Register<DexterContainerTest>(this.GetType().Assembly, LifeCycle.Singleton), Times.Once());
		}

		[Fact]
		public void RegisterByGenericsByKeyAndLifeCycleEnum_WithValidData_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.Register<DexterContainerTest, DexterContainerTest>(LifeCycle.Singleton);

			this.mockContainer.Verify(x => x.Register<DexterContainerTest, DexterContainerTest>(LifeCycle.Singleton), Times.Once());
		}

		[Fact]
		public void RegisterByKeyAndTypeAndTypeAndLifeCycleEnum_WithNullImplementedType_ShouldThrowArgumentNullException()
		{
			Executing.This(() => DexterContainer.Register(typeof(DexterContainerTest), null, LifeCycle.Singleton)).Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void RegisterByKeyAndTypeAndTypeAndLifeCycleEnum_WithNullType_ShouldThrowArgumentNullException()
		{
			Executing.This(() => DexterContainer.Register(null, typeof(DexterContainerTest), LifeCycle.Singleton)).Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void RegisterByKeyAndTypeAndTypeAndLifeCycleEnum_WithValidData_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.Register(typeof(DexterContainerTest), typeof(DexterContainerTest), LifeCycle.Singleton);

			this.mockContainer.Verify(x => x.Register(typeof(DexterContainerTest), typeof(DexterContainerTest), LifeCycle.Singleton), Times.Once());
		}

		[Fact]
		public void RegisterComponentsByBaseClass_WithNullAssembly_ShouldThrowArgumentNullException()
		{
			Executing.This(() => DexterContainer.RegisterComponentsByBaseClass<DexterContainerTest>(null, LifeCycle.Singleton)).Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void RegisterComponentsByBaseClass_WithValidData_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.RegisterComponentsByBaseClass<DexterContainerTest>(this.GetType().Assembly, LifeCycle.Singleton);

			this.mockContainer.Verify(x => x.RegisterComponentsByBaseClass<DexterContainerTest>(this.GetType().Assembly, LifeCycle.Singleton));
		}

		[Fact]
		public void Release_WithNullInstance_ShouldThrowArgumentNullExceprtion()
		{
			Executing.This(() => DexterContainer.Release(null)).Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Release_WithValidData_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.Release(this);
			this.mockContainer.Verify(x => x.Release(this), Times.Once());
		}

		[Fact]
		public void ResolveAllByType_WithNullType_ShouldThrowArgumentNullException()
		{
			Executing.This(() => DexterContainer.ResolveAll(null)).Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void ResolveAllByType_WithValidData_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.ResolveAll(typeof(DexterContainerTest));

			this.mockContainer.Verify(x => x.ResolveAll(typeof(DexterContainerTest)));
		}

		[Fact]
		public void ResolveAll_WithValidData_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.ResolveAll<DexterContainerTest>();

			this.mockContainer.Verify(x => x.ResolveAll<DexterContainerTest>(), Times.Once());
		}

		[Fact]
		public void ResolveByGenerics_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.Resolve<DexterContainerTest>();

			this.mockContainer.Verify(x => x.Resolve<DexterContainerTest>(), Times.Once());
		}

		[Fact]
		public void ResolveByTypeAndKey_WithNullType_ShouldThrowArgumenNullException()
		{
			Executing.This(() => DexterContainer.Resolve(null)).Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void ResolveByTypeAndKey_WithValidData_ShouldInvokeMockMethod()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.Resolve(typeof(DexterContainerTest));

			this.mockContainer.Verify(x => x.Resolve(typeof(DexterContainerTest)), Times.Once());
		}

		[Fact]
		public void ResolveByType_WithNullType_ShouldThrowArgumenNullException()
		{
			Executing.This(() => DexterContainer.Resolve(null)).Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void SetCurrent_ShouldChangeTheCurrentContainer()
		{
			DexterContainer.SetCurrent(this.mockContainer.Object);

			DexterContainer.Engine.Should().Be.EqualTo(this.mockContainer.Object);
		}

		#endregion
	}
}