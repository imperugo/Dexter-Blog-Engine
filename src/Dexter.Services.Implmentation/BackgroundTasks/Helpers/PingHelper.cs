#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PingHelper.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/31
// Last edit:	2013/03/31
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Implmentation.BackgroundTasks.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml;

	using Dexter.Data;
	using Dexter.Dependency;
	using Dexter.Entities;

	internal static class PingHelper
	{
		#region Static Fields

		private static readonly IConfigurationDataService configurationDataService;

		private static readonly IPingDataService pingDataService;

		#endregion

		#region Constructors and Destructors

		static PingHelper()
		{
			pingDataService = DexterContainer.Resolve<IPingDataService>();
			configurationDataService = DexterContainer.Resolve<IConfigurationDataService>();
		}

		#endregion

		#region Public Methods and Operators

		public static async Task NotifySites()
		{
			BlogConfigurationDto configuration = configurationDataService.GetConfiguration();
			IEnumerable<Uri> pingSites = pingDataService.GetPingSites();

			foreach (Uri pingSite in pingSites)
			{
				using (HttpClient client = new HttpClient())
				{
					await client.PostAsXmlAsync(pingSite.ToString(), GetRequest(pingSite, configuration));
				}
			}
		}

		#endregion

		#region Methods

		private static XmlDocument GetRequest(Uri url, BlogConfigurationDto configuration)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				using (XmlTextWriter writer = new XmlTextWriter(ms, Encoding.ASCII))
				{
					writer.WriteStartDocument();
					writer.WriteStartElement("methodCall");
					writer.WriteElementString("methodName", "weblogUpdates.ping");
					writer.WriteStartElement("params");
					writer.WriteStartElement("param");
					writer.WriteElementString("value", configuration.Name);
					writer.WriteEndElement();
					writer.WriteStartElement("param");
					writer.WriteElementString("value", url.ToString());
					writer.WriteEndElement();
					writer.WriteEndElement();
					writer.WriteEndElement();
				}

				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(ms);

				return xmlDocument;
			}
		}

		#endregion
	}
}