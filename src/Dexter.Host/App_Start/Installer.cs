using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dexter.Host.App_Start
{
	using System.Web.Mvc;

	using Dexter.Dependency;
	using Dexter.Dependency.Installation;

	public class Installer : ILayerInstaller
	{
		public void ApplicationStarted(IDexterContainer container)
		{
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.RegisterComponentsByBaseClass<Controller>(this.GetType().Assembly, LifeCycle.Transient);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
		}
	}
}