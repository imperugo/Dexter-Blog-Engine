#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ILayerInstaller.cs
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

namespace Dexter.Dependency.Installation
{
	/// <summary>
	/// 	The interface used for component registration
	/// </summary>
	public interface ILayerInstaller
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name="container"> The container. </param>
		void ApplicationStarted(IDexterContainer container);

		/// <summary>
		/// 	Installs all the component needed by the assembly.
		/// </summary>
		/// <param name="container"> The container. </param>
		void ServiceRegistration(IDexterContainer container);

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name="container"> The container. </param>
		void ServiceRegistrationComplete(IDexterContainer container);

		#endregion
	}
}