#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IThemeHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/05
// Last edit:	2013/05/05
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Theme
{
	public interface IThemeHelper
	{
		Theme CurrentTheme();
	}

	public class ThemeHelper : IThemeHelper
	{
		public Theme CurrentTheme()
		{
			return new Theme()
				       {
					       Name = "ScoreMe",
					       Templates = new[] { "Layout" },
					       Widgets = new[] { "TopTags" }
				       };
		}
	}
}