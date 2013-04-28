#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LayerInstaller.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Api.Core
{
	using System.Net.Http.Formatting;
	using System.Web.Http;
	using System.Web.Http.Filters;

	using Dexter.Api.Core.Attributes;
	using Dexter.Api.Core.Formatters.Syndication;
	using Dexter.Dependency;
	using Dexter.Dependency.Installation;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	public class LayerInstaller : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
			GlobalConfiguration.Configuration.Formatters.Add(new SyndicationFeedFormatter());

			// Configure json response
			JsonMediaTypeFormatter json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
			json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<FilterAttribute, ExceptionHandlingAttribute>(LifeCycle.Singleton);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
		}

		#endregion
	}
}