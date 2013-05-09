#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			UpdateDenormalizedItemIndex.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/03
// Last edit:	2013/03/23
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Indexes.Updating
{
	using Dexter.Data.Raven.Domain;
	using Dexter.Shared.Dto;

	using global::Raven.Abstractions.Data;

	using global::Raven.Client;

	using global::Raven.Client.Linq;

	using global::Raven.Json.Linq;

	internal class UpdateDenormalizedItemIndex
	{
		#region Public Methods and Operators

		public static void UpdateIndexes(IDocumentStore store, IDocumentSession session, Item item)
		{
			if (item.IsTransient)
			{
				store.DatabaseCommands.Patch(
					item.CommentsId, 
					new[]
						{
							new PatchRequest
								{
									Type = PatchCommandType.Set, 
									Name = "Item", 
									Value = RavenJObject.FromObject(new ItemReference
										                                {
											                                Id = item.Id, 
											                                Status = item.Status, 
											                                ItemPublishedAt = item.PublishAt
										                                })
								}
						});

				store.DatabaseCommands.Patch(
					item.TrackbacksId, 
					new[]
						{
							new PatchRequest
								{
									Type = PatchCommandType.Set, 
									Name = "Item", 
									Value = RavenJObject.FromObject(new ItemReference
										                                {
											                                Id = item.Id, 
											                                Status = item.Status, 
											                                ItemPublishedAt = item.PublishAt
										                                })
								}
						});
			}

			if (item is Post)
			{
				Post p = (Post)item;

				if (item.Status != ItemStatus.Published)
				{
					return;
				}

				foreach (string c in p.Categories)
				{
					store.DatabaseCommands.UpdateByIndex("Category/PostIds", 
						new IndexQuery
							{
								Query = session.Query<Category>().Where(x => x.Name == c).ToString()
							}, new[]
								   {
									   new PatchRequest
										   {
											   Type = PatchCommandType.Remove, 
											   Name = "PostsId", 
											   Value = p.Id
										   }, 
									   new PatchRequest
										   {
											   Type = PatchCommandType.Add, 
											   Name = "PostsId", 
											   Value = p.Id
										   }
								   }, allowStale: true);
				}
			}
		}

		#endregion
	}
}