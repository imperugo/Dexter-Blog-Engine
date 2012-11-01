#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CultureDictionary.cs
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

namespace Dexter.Localization.Po
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;

	[Serializable]
	internal class CultureDictionary
	{
		#region Public Properties

		public CultureInfo Culture { get; set; }

		public IDictionary<string, string> Translations { get; set; }

		#endregion
	}
}