#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ISetupService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/23
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Services
{
	using System.Threading.Tasks;

	using Dexter.Dependency.Attributes;
	using Dexter.Dependency.Castle.Interceptor;
	using Dexter.Dependency.Validator;
	using Dexter.Services.Requests;
	using Dexter.Shared.Dto;

	public interface ISetupService : IValidate
	{
		#region Public Properties

		bool IsInstalled { get; }

		#endregion

		#region Public Methods and Operators

		Task InitializeAsync([Validate] SetupRequest item);

		#endregion
	}
}