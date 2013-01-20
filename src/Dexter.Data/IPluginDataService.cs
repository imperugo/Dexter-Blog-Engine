#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IPluginDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/31
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data
{
	using System;

	using Dexter.Entities;
	using Dexter.Entities.Result;

	public interface IPluginDataService
	{
		#region Public Methods and Operators

		void DisablePlugin(PluginDto item);

		void EnablePlugin(PluginDto item);

		IPagedResult<PluginDto> GetInstalledPlugin(int pageIndex, int pageSize);

		PluginDto GetPlugin(string packageId, Version version);

		void UpdatePlugin(PluginDto item);

		#endregion
	}
}