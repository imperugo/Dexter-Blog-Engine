#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TrackbackDto.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/11
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Shared.Dto
{
	using System;

	public class TrackBackDto
	{
		#region Public Properties

		public string Excerpt { get; set; }

		public bool IsSpam { get; set; }

		public int ItemId { get; set; }

		public string Name { get; set; }

		public TrackbackStatus Status { get; set; }

		public string Title { get; set; }

		public Uri Url { get; set; }

		#endregion
	}
}