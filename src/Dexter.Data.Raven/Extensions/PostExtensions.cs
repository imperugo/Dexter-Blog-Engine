#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/03
// Last edit:	2012/11/03
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Extensions
{
	using Dexter.Data.Raven.Domain;
	using Dexter.Entities;

	internal static class PostExtensions
	{
		#region Public Methods and Operators

		public static bool MustUpdateDenormalizedObject(this Post target, PostDto source)
		{
			if (!target.Slug.Equals(source.Slug))
			{
				return true;
			}

			if (!target.Title.Equals(source.Title))
			{
				return true;
			}

			if (!target.PublishAt.Equals(source.PublishAt))
			{
				return true;
			}

			return false;
		}

		#endregion
	}
}