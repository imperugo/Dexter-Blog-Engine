#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LayerInstaller.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/01/07
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.PackageInstaller
{
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;
	using Dexter.PackageInstaller.Automapper;
	using Dexter.Services;

	using NuGet;

	public class LayerInstaller : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
			
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<IPackageManager, PackageManager>(LifeCycle.Singleton);
			container.Register<IPackageInstaller, Services.PackageInstaller>(LifeCycle.Singleton);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
			AutoMapperConfiguration.Configure();
		}

		#endregion
	}
}