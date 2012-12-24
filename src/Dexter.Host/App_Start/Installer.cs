#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host.App_Start
{
	using System.Web.Mvc;

	using Dexter.Dependency;
	using Dexter.Dependency.Installation;
	using Dexter.Host.App_Start.Automapper;

	public class Installer : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.RegisterComponentsByBaseClass<Controller>(this.GetType().Assembly, LifeCycle.Transient);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
			AutoMapperConfiguration.Configure();
		}

		#endregion
	}
}