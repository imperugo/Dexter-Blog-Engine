#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Link.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
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

	public enum BlogRollFamilyType : short
	{
		/// <summary>
		/// 	Nothing is used for specify that 
		/// 	the blogroll in not for a family person
		/// </summary>
		Nothing = 0, 

		/// <summary>
		/// 	Child BlogRoll
		/// </summary>
		Child = 1, 

		/// <summary>
		/// 	Parent BlogRoll
		/// </summary>
		Parent = 2, 

		/// <summary>
		/// 	Sibling BlogRoll
		/// </summary>
		Sibling = 3, 

		/// <summary>
		/// 	Spouse BlogRoll
		/// </summary>
		Spouse = 4, 

		/// <summary>
		/// 	Kin BlogRoll
		/// </summary>
		Kin = 5
	}

	public enum BlogRollFriendType : short
	{
		Nothing = 0, 

		Contact = 1, 

		Friend = 2, 

		Acquaintance = 3
	}

	public enum BlogRollGeographicalType : short
	{
		Nothing = 0, 

		Co_resident = 1, 

		Neighbor = 2
	}

	public class Link : EntityBase<int>
	{
		#region Public Properties

		public virtual bool? CoWorker { get; set; }

		public virtual bool? Colleague { get; set; }

		public virtual bool? Crush { get; set; }

		public virtual bool? Date { get; set; }

		public virtual BlogRollFamilyType? FamilyType { get; set; }

		public virtual BlogRollFriendType? FriendType { get; set; }

		public virtual BlogRollGeographicalType? GeographicalType { get; set; }

		public virtual bool? IsMyBlog { get; set; }

		public virtual bool? Met { get; set; }

		public virtual bool? Muse { get; set; }

		public virtual string Name { get; set; }

		public virtual int Position { get; set; }

		public virtual bool? Sweetheart { get; set; }

		public virtual Uri Url { get; set; }

		#endregion
	}
}