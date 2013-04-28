#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ServicesController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/12/31
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Controllers
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Mvc;
	using System.Xml;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Entities;
	using Dexter.Services;
	using Dexter.Services.Implmentation;
	using Dexter.Web.Core.Controllers;

	public class ServicesController : DexterControllerBase
	{
		#region Fields

		private readonly IConfigurationService configurationService;

		private readonly ITrackbackService trackbackService;

		#endregion

		#region Constructors and Destructors

		public ServicesController(ILog logger, IConfigurationService configurationService, ITrackbackService trackbackService)
			: base(logger, configurationService)
		{
			this.configurationService = configurationService;
			this.trackbackService = trackbackService;
		}

		#endregion

		#region Public Methods and Operators

		public async Task<ActionResult> Pingback()
		{
			if (!this.BlogConfiguration.Tracking.EnablePingBackReceive)
			{
				this.Logger.Debug("PingBack Receive is disabled, returning 404.");
				throw new HttpException(404, "Page not found");
			}

			XmlDocument doc = this.RetrieveXmlDocument();
			XmlNodeList list = doc.SelectNodes("methodCall/params/param/value/string") ?? doc.SelectNodes("methodCall/params/param/value");

			if (list != null)
			{
				try
				{
					string sourceUrl = list[0].InnerText.Trim();
					string targetUrl = list[1].InnerText.Trim();

					string title = await this.ExamineSourcePage(sourceUrl);

					if (string.IsNullOrEmpty(title))
					{
						this.SendError(16, "The source URI does not exist.");
						return new EmptyResult();
					}

					this.HttpContext.Response.ContentType = "text/xml";

					Uri url = new Uri(targetUrl);

					string postUrl = url.Segments[url.Segments.Length - 1];

					TrackBackDto trackBackDto = new TrackBackDto
						                            {
							                            Url = new Uri(postUrl), 
							                            Title = title, 
						                            };

					await this.trackbackService.SaveOrUpdateAsync(trackBackDto, ItemType.Post);

					this.HttpContext.Response.Write("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><response><error>0</error></response>");
					this.HttpContext.Response.End();
				}
				catch (DuplicateTrackbackException)
				{
					this.SendError(48, "The pingback has already been registered.");
				}
				catch (SpamException)
				{
					this.SendError(17, "The source URI does not contain a link to the target URI, and so cannot be used as a source.");
				}
				catch (Exception e)
				{
					this.Logger.Error("Error during saving pingback", e);
					this.SendError(0, "Ops, something wrong");
				}
			}

			this.SendError(0, "Ops, something wrong");

			return new EmptyResult();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public async Task<ActionResult> Trackback(int id, string title, string excerpt, string blog_name, string url, ItemType itemType = ItemType.Post)
		{
			if (!this.BlogConfiguration.Tracking.EnableTrackBackReceive)
			{
				this.Logger.Debug("Trackback Receive is disabled, returning 404.");
				throw new HttpException(404, "Page not found");
			}

			BlogConfigurationDto configuration = this.configurationService.GetConfiguration();

			if (!configuration.Tracking.EnableTrackBackReceive)
			{
				return this.HttpNotFound();
			}

			if (url != null)
			{
				url = url.Split(',')[0];
			}

			if (url == null)
			{
				return this.HttpNotFound();
			}

			TrackBackDto trackBackDto = new TrackBackDto
				                            {
					                            Url = new Uri(url), 
					                            Title = title, 
					                            Excerpt = excerpt
				                            };

			try
			{
				await this.trackbackService.SaveOrUpdateAsync(trackBackDto, itemType);

				this.HttpContext.Response.Write("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><response><error>0</error></response>");
				this.HttpContext.Response.End();
			}
			catch (DuplicateTrackbackException)
			{
				this.HttpContext.Response.Write("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><response><error>Trackback already registered</error></response>");
				this.HttpContext.Response.End();
			}
			catch (SpamException)
			{
				this.HttpContext.Response.Write("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><response><error>The source page does not contain the link</error></response>");
				this.HttpContext.Response.End();
			}

			return new EmptyResult();
		}

		#endregion

		#region Methods

		private async Task<string> ExamineSourcePage(string sourceUrl)
		{
			try
			{
				using (HttpClientHandler handler = new HttpClientHandler())
				{
					handler.AllowAutoRedirect = true;
					handler.MaxAutomaticRedirections = 4;
					handler.UseCookies = true;
					handler.CookieContainer = new CookieContainer();
					handler.Credentials = CredentialCache.DefaultNetworkCredentials;
					using (HttpClient client = new HttpClient(handler))
					{
						using (HttpResponseMessage response = await client.GetAsync(sourceUrl))
						{
							string html = await response.Content.ReadAsAsync<string>();

							Regex regexTitle = new Regex(@"<title.*?>(?<title>.*)</title>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

							Match match = regexTitle.Match(html);

							if (match.Success)
							{
								return match.Groups["title"].Value
								                            .Trim()
								                            .Replace(@"\r", string.Empty)
								                            .Replace(@"\n", string.Empty);
							}

							return null;
						}
					}
				}
			}
			catch (Exception e)
			{
				this.Logger.Error("Something wrong during pingback check", e);
				return null;
			}
		}

		private string ParseRequest()
		{
			byte[] buffer = new byte[this.HttpContext.Request.InputStream.Length];
			this.HttpContext.Request.InputStream.Read(buffer, 0, buffer.Length);

			return Encoding.Default.GetString(buffer);
		}

		private XmlDocument RetrieveXmlDocument()
		{
			string xml = this.ParseRequest();
			if (!xml.Contains("<methodName>pingback.ping</methodName>"))
			{
				throw new HttpException(404, "File Not Found.");
			}

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			return doc;
		}

		private void SendError(int code, string message)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<?xml version=\"1.0\"?>");
			sb.Append("<methodResponse>");
			sb.Append("<fault>");
			sb.Append("<value>");
			sb.Append("<struct>");
			sb.Append("<member>");
			sb.Append("<name>faultCode</name>");
			sb.AppendFormat("<value><int>{0}</int></value>", code);
			sb.Append("</member>");
			sb.Append("<member>");
			sb.Append("<name>faultString</name>");
			sb.AppendFormat("<value><string>{0}</string></value>", message);
			sb.Append("</member>");
			sb.Append("</struct>");
			sb.Append("</value>");
			sb.Append("</fault>");
			sb.Append("</methodResponse>");

			this.HttpContext.Response.Write(sb.ToString());
		}

		#endregion
	}
}