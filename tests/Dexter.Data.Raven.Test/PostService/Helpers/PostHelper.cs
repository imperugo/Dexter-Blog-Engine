#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Test.PostService.Helpers
{
	using System.Collections.Generic;

	using Dexter.Data.Raven.Domain;

	using FizzWare.NBuilder;

	internal static class PostHelper
	{
		#region Public Methods and Operators

		public static IList<Post> GetPosts(int numberOfDocument)
		{
			return Builder<Post>.CreateListOfSize(numberOfDocument).Build();
		}

		#endregion
	}
}