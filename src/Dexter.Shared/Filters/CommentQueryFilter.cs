#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CommentQueryFilter.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Shared.Filters
{
	using Dexter.Shared.Dto;

	public class CommentQueryFilter
	{
		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Object" /> class.
		/// </summary>
		public CommentQueryFilter()
		{
			this.CommentStatus = CommentStatus.IsApproved;
		}

		#endregion

		#region Public Properties

		public CommentStatus CommentStatus { get; set; }

		#endregion
	}
}