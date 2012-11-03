#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			EntityBase.cs
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

namespace Dexter.Data.Raven.Domain
{
	using System;
	using System.Collections.Generic;

	public class EntityBase<T>
	{
		#region Public Properties

		public DateTimeOffset CreatedAt { get; set; }

		public T Id { get; set; }

		#endregion

		public bool IsTransient
		{
			get
			{
				return EqualityComparer<T>.Default.Equals(this.Id, default(T));
			}
		}
	}
}