#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Plugin.cs
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

namespace Dexter.Data.Raven.Domain
{
	using System;

	public class Plugin : EntityBase<int>
	{
		#region Public Properties

		public virtual string Author { get; set; }

		public string Description { get; set; }

		public bool Enabled { get; set; }

		public virtual bool IsInstalled { get; protected set; }

		public string Name { get; set; }

		public virtual Version Version { get; protected set; }

		public virtual Uri Website { get; set; }

		#endregion
	}
}