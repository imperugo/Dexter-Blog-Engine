#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			UpdateCategoryIndex.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/01
// Last edit:	2012/12/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Indexes.Updating
{
	using Dexter.Data.Raven.Domain;

	using global::Raven.Abstractions.Data;

	using global::Raven.Client;

	using global::Raven.Client.Linq;

	internal class UpdateCategoryIndex
	{
		#region Public Methods and Operators

		public static void UpdateCategoryIndexAfterDelete(IDocumentStore store, IDocumentSession session, Category deletedCategory, Category newCategory)
		{
			if (deletedCategory.ChildrenIds != null)
			{
				store.DatabaseCommands.UpdateByIndex("Category/ById", 
					new IndexQuery
						{
							Query = session.Query<Category>()
							               .Where(x => x.Id.In(deletedCategory.ChildrenIds))
							               .ToString()
						}, 
					new[]
						{
							new PatchRequest
								{
									Type = PatchCommandType.Set, 
									Name = "ParentId", 
									Value = newCategory.Id
								}
						}, allowStale: true);
			}

			store.DatabaseCommands.UpdateByIndex("Category/ById",
				new IndexQuery
					{
						Query = session.Query<Category>()
						               .Where(x => x.Id == deletedCategory.ParentId)
						               .ToString()
					},
				new[]
					{
						new PatchRequest
							{
								Type = PatchCommandType.Remove,
								Name = "ChildrenIds",
								Value = deletedCategory.Id
							},
						new PatchRequest
							{
								Type = PatchCommandType.Add,
								Name = "ChildrenIds",
								Value = newCategory.Id
							}
					}, allowStale: true);
		}

		public static void UpdateCategoryIndexes(IDocumentStore store, IDocumentSession session, Category item)
		{
			if (!string.IsNullOrEmpty(item.ParentId))
			{
				store.DatabaseCommands.Patch(
					item.ParentId, 
					new[]
						{
							new PatchRequest
								{
									Type = PatchCommandType.Add, 
									Name = "ChildrenIds", 
									Value = item.Id
								}
						});
			}

			store.DatabaseCommands.UpdateByIndex("Category/ById", 
				new IndexQuery
					{
						Query = session.Query<Category>().Where(x => x.Id != item.Id).ToString()
					}, 
				new[]
					{
						new PatchRequest
							{
								Type = PatchCommandType.Set, 
								Name = "IsDefault", 
								Value = false
							}
					}, allowStale: true);
		}

		#endregion
	}
}