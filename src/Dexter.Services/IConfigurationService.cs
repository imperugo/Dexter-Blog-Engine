#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IConfigurationService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services
{
	using System.Threading.Tasks;

	using Dexter.Entities;

	public interface IConfigurationService
	{
		#region Public Methods and Operators

		BlogConfiguration GetConfiguration();

		Task<BlogConfiguration> GetConfigurationAsync();

		#endregion
	}
}