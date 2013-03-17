#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IConfigurationService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/28
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services
{
	using System.Security.Permissions;
	using System.Threading.Tasks;

	using Dexter.Entities;
	using Dexter.Shared;

	public interface IConfigurationService
	{
		#region Public Methods and Operators

		BlogConfigurationDto GetConfiguration();

		[PrincipalPermission(SecurityAction.PermitOnly, Authenticated = true, Role = Constants.AdministratorRole)]
		void SaveOrUpdateConfiguration(BlogConfigurationDto item);

		#endregion
	}
}