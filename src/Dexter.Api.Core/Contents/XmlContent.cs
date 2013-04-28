#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			XmlContent.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/28
// Last edit:	2013/04/28
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Api
{
	using System.IO;
	using System.Net;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
	using System.Xml;

	public class XmlContent : HttpContent
	{
		#region Fields

		private readonly MemoryStream ms = new MemoryStream();

		#endregion

		#region Constructors and Destructors

		public XmlContent(XmlDocument document)
		{
			document.Save(this.ms);
			this.ms.Position = 0;
			this.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
		}

		#endregion

		#region Methods

		protected override void Dispose(bool disposing)
		{
			this.ms.Dispose();
			base.Dispose(disposing);
		}

		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			this.ms.CopyTo(stream);

			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			tcs.SetResult(null);
			return tcs.Task;
		}

		protected override bool TryComputeLength(out long length)
		{
			length = this.ms.Length;
			return true;
		}

		#endregion
	}
}