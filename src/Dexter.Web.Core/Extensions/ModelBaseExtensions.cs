#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ModelBaseExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/08
// Last edit:	2013/05/08
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Models
{
	using Dexter.Shared.Dto;

	public static class ModelBaseExtensions
	{
		public static void ApplySeoInfoFromItem(this DexterModelBase model, ItemDto item)
		{
			model.Title = item.Title;
			model.Description = item.Excerpt;
			model.KeyWords = string.Join(",", item.Tags);
			model.Author = item.Author.Username;
		}
	}
}