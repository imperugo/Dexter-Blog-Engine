#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			FakeDataController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/11/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Controllers
{
	using System;
	using System.Collections.ObjectModel;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	public class FakeDataController : DexterControllerBase
	{
		#region Public Methods and Operators

		public FakeDataController(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService)
			: base(logger, configurationService, postService, commentService)
		{
		}

		public ActionResult Index()
		{
			PostDto item = new PostDto();
			item.Title = "Post Title";
			item.Abstract = "Post abstract";
			item.Content = "This is the body of a fake post";
			item.PublishAt = DateTime.Today;
			item.Slug = "PostSlug";
			item.Status = ItemStatus.Published;
			item.Tags = new Collection<string>()
				            {
					            "tag1",
					            "tag2"
				            };

			this.PostService.SaveOrUpdate(item);

			return this.Content("Fake data added");
		}

		#endregion
	}
}