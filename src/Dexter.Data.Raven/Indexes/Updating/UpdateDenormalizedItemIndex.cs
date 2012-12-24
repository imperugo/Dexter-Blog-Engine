#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			UpdateDenormalizedItemIndex.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/03
// Last edit:	2012/12/24
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

	using global::Raven.Json.Linq;

	internal class UpdateDenormalizedItemIndex
	{
		#region Public Methods and Operators

		public static void UpdateIndexes(IDocumentStore store, IDocumentSession session, Item item)
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
		}

		#endregion
	}
}