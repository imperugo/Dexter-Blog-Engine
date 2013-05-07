#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Theme.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/06
// Last edit:	2013/05/06
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Theme
{
	using System.Linq;

	public class Theme
	{
		public string Name { get; set; }
		public string[] Templates { get; set; }
		public string[] Widgets { get; set; }

		public bool HasView(string viewName)
		{
			if (Templates.Any(x => x == viewName))
			{
				return true;
			}

			if (Widgets.Any(x => x == viewName))
			{
				return true;
			}

			return false;
		}
	}
}