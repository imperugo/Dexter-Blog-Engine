#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IUserContext.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/14
// Last edit:	2013/03/14
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Shared.UserContext
{
	public interface IUserContext
	{
		#region Public Properties

		bool IsAuthenticated { get; }

		string Username { get; }

		#endregion

		#region Public Methods and Operators

		bool IsInRole(string roleName);

		#endregion
	}
}