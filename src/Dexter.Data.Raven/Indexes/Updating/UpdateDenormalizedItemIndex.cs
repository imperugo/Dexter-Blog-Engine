using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dexter.Data.Raven.Indexes.Updating
{
	using Dexter.Data.Raven.Domain;

	using global::Raven.Abstractions.Data;
	using global::Raven.Client;
	using global::Raven.Json.Linq;

	internal class UpdateDenormalizedItemIndex
	{
		public static void UpdateIndexes(IDocumentStore store, IDocumentSession session, Item item)
		{
			store.DatabaseCommands.UpdateByIndex("UpdateDenormalizedItems",
																new IndexQuery()
																	{
																		Query = session.Query<ItemComments>().Where(x => x.Id == item.CommentsId).ToString()
																	}, 
																 new[]
																	{
																			new PatchRequest
																			{
																					Type = PatchCommandType.Modify,
																					Name = "Mentor",
																					Nested = new[]
																							{
																									new PatchRequest
																									{
																											Type = PatchCommandType.Set,
																											Name = "Item",
																											Value = RavenJObject.FromObject(new ItemReference()
																												        {
																													        Id = item.Id,
																															Status = item.Status,
																															ItemPublishedAt = item.PublishAt
																												        })
																									},
																							}
																			}
																	});
		}
	}
}
