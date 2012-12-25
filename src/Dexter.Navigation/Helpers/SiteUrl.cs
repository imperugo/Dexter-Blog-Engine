#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SiteUrl.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Helpers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Web;

	public class SiteUrl : IHtmlString
	{
		#region Constructors and Destructors

		public SiteUrl(string domain, string controller, string action)
			: this(domain, 80, false, null, controller, action, null, null)
		{
		}

		public SiteUrl(string domain, string area, string controller, string action)
			: this(domain, 80, false, area, controller, action, null, null)
		{
		}

		public SiteUrl(string domain, string controller, string action, IDictionary<string, object> parameters)
			: this(domain, 80, false, null, controller, action, null, parameters)
		{
		}

		public SiteUrl(string domain, string area, string controller, string action, IDictionary<string, object> parameters)
			: this(domain, 80, false, area, controller, action, null, parameters)
		{
		}

		public SiteUrl(string domain, int port, bool isSecureConnection, string area, string controller, string action, string[] segments, IDictionary<string, object> parameters)
		{
			this.Domain = domain;
			this.Port = port;
			this.Protocol = isSecureConnection ? "https" : "http";
			this.IsSecureConnection = isSecureConnection;
			this.Action = action;
			this.Area = area;
			this.Controller = controller;
			this.Parameters = parameters;
			this.Segments = segments;
		}

		#endregion

		#region Public Properties

		public string Action { get; private set; }

		public string Area { get; private set; }

		public string Controller { get; private set; }

		public string Domain { get; private set; }

		public bool IsSecureConnection { get; private set; }

		public IDictionary<string, object> Parameters { get; private set; }

		public int Port { get; private set; }

		public string Protocol { get; private set; }

		public string[] Segments { get; private set; }

		#endregion

		#region Public Methods and Operators

		public static implicit operator string(SiteUrl url)
		{
			return url.ToString();
		}

		public SiteUrl ForceToHttps(int httpsPort = 443)
		{
			return new SiteUrl(this.Domain, httpsPort, true, this.Area, this.Controller, this.Action, this.Segments, this.Parameters);
		}

		public string ToHtmlString()
		{
			return this.ToString();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("{0}://{1}:{2}/", this.Protocol, this.Domain, this.Port);

			if (!string.IsNullOrEmpty(this.Area))
			{
				sb.AppendFormat("{0}/", this.Area);
			}

			sb.AppendFormat("{0}/", this.Controller);
			sb.AppendFormat("{0}/", this.Action);

			foreach (string segment in this.Segments)
			{
				sb.AppendFormat("{0}/", segment);
			}

			if (this.Parameters != null && this.Parameters.Any())
			{
				sb.Append("?d=u&");
				foreach (KeyValuePair<string, object> parameter in this.Parameters)
				{
					sb.AppendFormat("&{0}={1}", HttpUtility.UrlEncode(parameter.Key), HttpUtility.UrlEncode(parameter.Value.ToString()));
				}
			}

			return sb.ToString();
		}

		#endregion
	}
}