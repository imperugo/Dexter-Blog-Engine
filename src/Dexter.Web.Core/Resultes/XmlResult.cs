#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			XmlResult.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/27
// Last edit:	2013/04/27
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Resultes
{
	using System.Web.Mvc;
	using System.Xml;
	using System.Xml.Linq;

	public class XmlResult : ActionResult
	{
		#region Fields

		private readonly XDocument document;

		private readonly string etag;

		#endregion

		#region Constructors and Destructors

		public XmlResult(XDocument document, string etag)
		{
			this.document = document;
			this.etag = etag;
		}

		#endregion

		#region Public Methods and Operators

		public override void ExecuteResult(ControllerContext context)
		{
			if (this.etag != null)
			{
				context.HttpContext.Response.AddHeader("ETag", this.etag);
			}

			context.HttpContext.Response.ContentType = "text/xml";

			using (XmlWriter xmlWriter = XmlWriter.Create(context.HttpContext.Response.OutputStream))
			{
				this.document.WriteTo(xmlWriter);
				xmlWriter.Flush();
			}
		}

		#endregion
	}
}