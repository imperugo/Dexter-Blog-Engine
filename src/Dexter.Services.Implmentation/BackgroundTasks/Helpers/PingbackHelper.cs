#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PingbackHelper.cs
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
	using System.Linq;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml;

	using Dexter.Dependency;
	using Dexter.Shared.Dto;
	using Dexter.Navigation.Contracts;
	using Dexter.Navigation.Helpers;

	internal static class PingbackHelper
	{
		#region Static Fields

		private static readonly IUrlBuilder urlBuilder;

		#endregion

		#region Constructors and Destructors

		static PingbackHelper()
		{
			urlBuilder = DexterContainer.Resolve<IUrlBuilder>();
		}

		#endregion

		#region Public Methods and Operators

		public static async Task Notify(ItemDto item, Uri uri)
		{
			SiteUrl itemUrl = item is PostDto
				                  ? urlBuilder.Post.Permalink(item)
				                  : urlBuilder.Page.Permalink(item);

			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(uri);

				KeyValuePair<string, IEnumerable<string>> header = response.Headers.SingleOrDefault(h => h.Key.Equals("x-pingback", StringComparison.OrdinalIgnoreCase) || h.Key.Equals("pingback", StringComparison.OrdinalIgnoreCase));

				string urlToPing = header.Value.FirstOrDefault();

				if (string.IsNullOrEmpty(urlToPing))
				{
					return;
				}

				var xmlRequest = GetXmlRequest(itemUrl, uri);

				await client.PostAsXmlAsync(urlToPing, xmlRequest);
			}
		}

		#endregion

		#region Methods

		private static XmlDocument GetXmlRequest(SiteUrl itemUrl, Uri uri)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				using (XmlTextWriter writer = new XmlTextWriter(ms, Encoding.ASCII))
				{
					writer.WriteStartDocument(true);
					writer.WriteStartElement("methodCall");
					writer.WriteElementString("methodName", "pingback.ping");
					writer.WriteStartElement("params");

					writer.WriteStartElement("param");
					writer.WriteStartElement("value");
					writer.WriteElementString("string", itemUrl.ToString());
					writer.WriteEndElement();
					writer.WriteEndElement();

					writer.WriteStartElement("param");
					writer.WriteStartElement("value");
					writer.WriteElementString("string", uri.ToString());
					writer.WriteEndElement();
					writer.WriteEndElement();

					writer.WriteEndElement();
					writer.WriteEndElement();
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(ms);

					return xmlDocument;
				}
			}
		}

		#endregion
	}
}