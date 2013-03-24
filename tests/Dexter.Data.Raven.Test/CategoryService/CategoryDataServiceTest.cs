#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CategoryDataServiceTest.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
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

	using Common.Logging;

	using Dexter.Data.Raven.Domain;
	using Dexter.Data.Raven.Helpers;
	using Dexter.Data.Raven.Services;
	using Dexter.Data.Raven.Test.Helpers;
	using Dexter.Entities;

	using Moq;

	using global::Raven.Client;

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

		[Fact]
		public void DeleteCategory_UsingAThreeShould_ShouldUpdateChildrensProperties()
		{
			IList<Category> categories = CategoryHelper.GetCategories(10);
			List<Category> parents = categories.Skip(0).Take(5).ToList();
			List<Category> childrens = categories.Skip(5).Take(5).ToList();

			this.SetupData(x =>
				{
					foreach (Category c in childrens)
					{
						x.Store(c);
					}
				});

			parents[0].ChildrenIds = childrens.Select(x => x.Id).ToArray();

			this.SetupData(x =>
				{
					foreach (Category c in parents)
					{
						x.Store(c);
					}
				});

			this.WaitStaleIndexes();

			this.sut.DeleteCategory(parents[0].Id, parents[1].Id);
			this.sut.Session.SaveChanges();

			using (IDocumentSession testSession = this.DocumentStore.OpenSession())
			{
				Category replaceCategory = testSession.Load<Category>(parents[1].Id);

				replaceCategory
					.ChildrenIds
					.Should()
					.Not
					.Be.Null();

				replaceCategory
					.ChildrenIds
					.Count()
					.Should()
					.Be
					.EqualTo(5);

				Category[] chlCat = testSession.Load<Category>(replaceCategory.ChildrenIds);

				foreach (Category category in chlCat)
				{
					category.ParentId.Should().Be.EqualTo(replaceCategory.Id);
				}
			}
		}

		[Fact]
		public void DeleteCategory_UsingTheDefaultCategory_ShouldMoveTheDefaultAttributoToTheNewCategory()
		{
			IList<Category> categories = CategoryHelper.GetCategories(4);

			Category defaultCategory = CategoryHelper.GetCategories(1)[0];
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

			this.sut.DeleteCategory(defaultCategory.Id, categories[0].Id);
			this.sut.Session.SaveChanges();

			IList<Category> newCategories;

			using (IDocumentSession testSession = this.DocumentStore.OpenSession())
			{
				newCategories = testSession.Query<Category>().ToList();
			}

			newCategories.Count(x => x.IsDefault)
			             .Should()
			             .Be
			             .EqualTo(1);

			newCategories.Single(x => x.Id == categories[0].Id).IsDefault
			             .Should()
			             .Be
			             .True();
		}

		public new void Dispose()
		{
			this.sut.Session.Dispose();
		}

		[Fact]
		public void GetCategories_WithDataStructure_ShouldReturnValidTree()
		{
			IList<Category> categories = CategoryHelper.GetCategories(10);

			categories[0].Id = "Categories/1";
			categories[0].ChildrenIds = new[] { "Categories/6", "Categories/7" };

			categories[1].Id = "Categories/2";
			categories[1].ChildrenIds = new[] { "Categories/8", "Categories/9", "Categories/10" };

			categories[2].Id = "Categories/3";
			categories[3].Id = "Categories/4";
			categories[4].Id = "Categories/5";

			categories[5].Id = "Categories/6";
			categories[5].ParentId = "Categories/1";

			categories[6].Id = "Categories/7";
			categories[6].ParentId = "Categories/1";

			categories[7].Id = "Categories/8";
			categories[7].ParentId = "Categories/2";

			categories[8].Id = "Categories/9";
			categories[8].ParentId = "Categories/2";

			categories[9].Id = "Categories/10";
			categories[9].ParentId = "Categories/2";

			this.SetupData(x =>
				{
					foreach (Category category in categories)
					{
						x.Store(category);
					}
				});

			this.WaitStaleIndexes();

			IList<CategoryDto> cats = this.sut.GetCategoriesStructure();

			cats.Count.Should().Be.EqualTo(5);
			cats[0].Categories.Should().Not.Be.Null();
			cats[1].Categories.Should().Not.Be.Null();
			cats[0].Categories.Count.Should().Be.EqualTo(2);
			cats[1].Categories.Count.Should().Be.EqualTo(3);
		}

		#endregion
	}
}