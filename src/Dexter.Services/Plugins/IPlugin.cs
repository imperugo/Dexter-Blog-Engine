#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPlugin.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/13
// Last edit:	2013/03/13
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Plugins
{
	public interface IPlugin
	{
		#region Public Methods and Operators

		/// <summary>
		/// Initialize all the plugin needs (i.e. register jobs, dependencies and so on):
		/// </summary>
		void Initialize();

		/// <summary>
		/// It's called during the setup/update. It should contains all the plugin needs to work correctly (i.e.: database stuff, configuration and so on).
		/// </summary>
		void Setup();

		#endregion
	}
}