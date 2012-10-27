#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ReadingConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Domain
{
	public class ReadingConfiguration
	{
		#region Public Properties

		public string EncodingForPageAndFeed { get; set; }

		public int HomePageItemId { get; set; }

		public int NumberOfPostPerFeed { get; set; }

		public int NumberOfPostPerPage { get; set; }

		public bool ShowAbstractInFeed { get; set; }

		#endregion
	}
}