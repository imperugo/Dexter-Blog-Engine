#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			UpdateEmailStatus.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/31
// Last edit:	2012/12/31
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

	internal class UpdateEmailStatus
	{
		#region Public Methods and Operators

		public static void UpdateIndexes(IDocumentStore store, IDocumentSession session, EmailMessage item)
		{
			store.DatabaseCommands.Patch(
				"EmailMessages/" + item.Id, 
				new[]
					{
						new PatchRequest
							{
								Type = PatchCommandType.Set, 
								Name = "Status", 
								Value = "Sending..."
							}
					});
		}

		#endregion
	}
}