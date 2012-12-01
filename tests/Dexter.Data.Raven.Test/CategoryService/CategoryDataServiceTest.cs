#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryDataServiceTest.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/01
// Last edit:	2012/12/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test.CategoryService
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;

	using Common.Logging;

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Services;
	using Dexter.Data.Raven.Test.Helpers;

	using Moq;

	using SharpTestsEx;

	using Xunit;

	public class CategoryDataServiceTest : RavenDbTestBase, IDisposable
	{
		#region Fields

		private readonly TestSessionFactory sessionFactory;

		private readonly CategoryDataService sut;

		#endregion

		#region Constructors and Destructors

		public CategoryDataServiceTest()
		{
			this.sessionFactory = new TestSessionFactory(this.DocumentStore);
			this.sut = new CategoryDataService(new Mock<ILog>().Object, this.sessionFactory, this.DocumentStore);
		}

		#endregion

		#region Public Methods and Operators

		public new void Dispose()
		{
			this.sut.Session.Dispose();
		}

		[Fact]
		public void SaveOrUpdate_WithValidDataForNewCategory_ShouldSaveTheCategory()
		{
			IList<Category> categories = CategoryHelper.GetCategories(5);

			foreach (Category category in categories)
			{
				category.Id = this.sut.SaveOrUpdate(category.Name, false, category.ParentId, category.Id);
			}

			this.sut.Session.SaveChanges();

			Category[] cats;

			using (var testSession = this.DocumentStore.OpenSession())
			{
				cats = testSession.Load<Category>(categories.Select(x => x.Id).ToArray());
			}

			cats.Should().Not.Be.Null();
			cats.Count().Should().Be.EqualTo(5);
		}

		[Fact]
		public void SaveOrUpdate_WithValidData_ShouldUpdateDefaultCategory()
		{
			IList<Category> categories = CategoryHelper.GetCategories(4);

			var defaultCategory = CategoryHelper.GetCategories(1)[0];
			defaultCategory.IsDefault = true;

			categories.Add(defaultCategory);

			this.SetupData(x =>
				{
					foreach (Category c in categories)
					{
						x.Store(c);
					}
				});

			this.WaitStaleIndexes();

			this.sut.SaveOrUpdate(categories[0].Name, true, categories[0].ParentId, categories[0].Id);
			this.sut.Session.SaveChanges();

			IList<Category> newCategories;

			using (var testSession = this.DocumentStore.OpenSession())
			{
				newCategories = testSession.Query<Category>().ToList();
			}

			newCategories.Should().Not.Be.Null();
			newCategories.Count.Should().Be.EqualTo(5);

			newCategories.Single(x => x.Id == defaultCategory.Id)
						.IsDefault
						.Should()
						.Be
						.False();

			newCategories.Single(x => x.Id == categories[0].Id)
						.IsDefault
						.Should()
						.Be
						.True();
		}

		#endregion
	}
}