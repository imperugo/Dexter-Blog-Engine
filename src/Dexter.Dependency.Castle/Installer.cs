#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/DexterBlogEngine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency.Castle
{
	using System.Web;

	using global::Castle.MicroKernel.Lifestyle;

	using Dexter.Dependency.Installation;

	public class CastleInstaller : ILayerInstaller
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name="container"> The container. </param>
		public void ApplicationStarted(IDexterContainer container)
		{
		}

		/// <summary>
		/// 	Installs all the component needed by the assembly.
		/// </summary>
		/// <param name="container"> The container. </param>
		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<IHttpModule, PerWebRequestLifestyleModule>(LifeCycle.Singleton);
		}

		/// <summary>
		/// 	Setups the service.
		/// </summary>
		/// <param name="container"> The container. </param>
		public void ServiceRegistrationComplete(IDexterContainer container)
		{
		}

		#endregion
	}
}