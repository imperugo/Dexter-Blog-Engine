#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Trackback.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Domain
{
	using System;

	public class Trackback : EntityBase<int>
	{
		#region Public Properties

		public virtual string Excerpt { get; set; }

		public virtual bool IsSpam { get; set; }

		public virtual string Name { get; set; }

		public virtual string Title { get; set; }

		public virtual Uri Url { get; set; }

		#endregion
	}
}