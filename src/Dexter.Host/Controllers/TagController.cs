#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TagController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/11
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Host.Controllers
{
	using System.Threading.Tasks;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Host.Model.TagController;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	public class TagController : DexterControllerBase
	{
		#region Constructors and Destructors

		public TagController(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService)
			: base(logger, configurationService, postService, commentService)
		{
		}

		#endregion

		#region Public Methods and Operators

		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public async Task<ActionResult> Archive(string id, int page = 1)
		{
			if (string.IsNullOrEmpty(id))
			{
				return this.HttpNotFound();
			}

			ArchiveViewModel model = new ArchiveViewModel();
			model.Tag = id;

			model.Posts = await this.PostService.GetPostsByTagAsync(page, 10, id);

			return this.View(model);
		}

		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public async Task<ActionResult> Index()
		{
			IndexViewModel model = new IndexViewModel();

			model.Tags = await this.PostService.GetTopTagsForPublishedPostsAsync(50);

			return this.View(model);
		}

		#endregion
	}
}