#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ItemReference.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/02
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Domain
{
	using System;

	using Dexter.Shared.Dto;

	public class ItemReference
	{
		#region Public Properties

		public string Id { get; set; }

		public DateTimeOffset ItemPublishedAt { get; set; }

		public ItemStatus Status { get; set; }

		#endregion

		#region Public Methods and Operators

		public static implicit operator ItemReference(Item item)
		{
			return new ItemReference
				       {
					       Id = item.Id, 
					       Status = item.Status, 
					       ItemPublishedAt = item.PublishAt
				       };
		}

		#endregion
	}
}