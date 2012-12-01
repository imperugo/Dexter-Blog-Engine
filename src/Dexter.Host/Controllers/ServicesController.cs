#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ServicesController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/12/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Controllers
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Entities;
	using Dexter.Services;
	using Dexter.Services.Implmentation;
	using Dexter.Web.Core.Controllers.Web;

	public class ServicesController : DexterControllerBase
	{
		#region Fields

		private readonly IConfigurationService configurationService;

		private readonly IPageService pageService;

		private readonly ITrackbackService trackbackService;

		#endregion

		#region Constructors and Destructors

		public ServicesController(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService, IPageService pageService, ITrackbackService trackbackService)
			: base(logger, configurationService, postService, commentService)
		{
			this.configurationService = configurationService;
			this.pageService = pageService;
			this.trackbackService = trackbackService;
		}

		#endregion

		#region Public Methods and Operators

		public ActionResult Pingback()
		{
			return null;
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public async Task<ActionResult> Trackback(int id, string title, string excerpt, string blog_name, string url, ItemType itemType = ItemType.Post)
		{
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

				HttpContext.Response.Write("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><response><error>0</error></response>");
				HttpContext.Response.End();
			}
			catch (DuplicateTrackbackException)
			{
				HttpContext.Response.Write("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><response><error>Trackback already registered</error></response>");
				HttpContext.Response.End();
			}
			catch (SpamException)
			{
				HttpContext.Response.Write("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><response><error>The source page does not contain the link</error></response>");
				HttpContext.Response.End();
			}

			return this.HttpNotFound();
		}

		#endregion
	}
}