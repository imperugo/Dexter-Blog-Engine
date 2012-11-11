#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			HttpHandlerBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/11/11
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.HttpHandlers
{
	using System;
	using System.Net;
	using System.Text;
	using System.Web;

	/// <summary>
	///	The base class for the HttpHandlers
	/// </summary>
	public abstract class HttpHandlerBase : IHttpHandler
	{
		#region Fields

		private HttpContextWrapper wrapper;

		#endregion

		#region Public Properties

		/// <summary>
		/// 	Gets a value indicating whether another request can use the <see cref = "T:System.Web.IHttpHandler" /> instance.
		/// </summary>
		/// <value></value>
		/// <returns>true if the <see cref = "T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
		/// </returns>
		public virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref = "T:System.Web.IHttpHandler" /> interface.
		/// </summary>
		/// <param name = "context">An <see cref = "T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		public void ProcessRequest(HttpContext context)
		{
			wrapper = new HttpContextWrapper(context);

			this.ProcessRequest(this.wrapper);
		}

		/// <summary>
		/// 	Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref = "T:System.Web.IHttpHandler" /> interface.
		/// </summary>
		/// <param name = "context">An <see cref = "T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		public abstract void ProcessRequest(HttpContextBase context);

		#endregion

		#region Methods

		protected void AddHeaders(string contentType, int cacheDays, int hashCode)
		{
			this.wrapper.Response.ContentType = contentType;
			this.wrapper.Response.ContentEncoding = Encoding.UTF8;

			this.wrapper.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;
			this.wrapper.Response.Cache.SetCacheability(HttpCacheability.Public);
			this.wrapper.Response.Cache.SetExpires(DateTime.Now.AddDays(cacheDays));
			this.wrapper.Response.Cache.SetMaxAge(new TimeSpan(cacheDays, 0, 0, 0));
			this.wrapper.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

			string etag = string.Concat("\"", hashCode, "\"");
			string incomingEtag = this.wrapper.Request.Headers["If-None-Match"];

			this.wrapper.Response.Cache.SetETag(etag);
			this.wrapper.Response.Cache.SetCacheability(HttpCacheability.Public);

			if (String.CompareOrdinal(incomingEtag, etag) == 0)
			{
				this.wrapper.Response.Clear();
				this.wrapper.Response.StatusCode = (int)HttpStatusCode.NotModified;
				this.wrapper.Response.SuppressContent = true;
			}
		}

		#endregion
	}
}