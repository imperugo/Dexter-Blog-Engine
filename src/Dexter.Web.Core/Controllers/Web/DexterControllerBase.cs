#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterControllerBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/12/24
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Web.Core.Controllers.Web
{
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Services;

	public class DexterControllerBase : AsyncController
	{
		#region Fields

		private readonly ICommentService commentService;

		private readonly IConfigurationService configurationService;

		private readonly ILog logger;

		private readonly IPostService postService;

		#endregion

		#region Constructors and Destructors

		public DexterControllerBase(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService)
		{
			this.logger = logger;
			this.configurationService = configurationService;
			this.postService = postService;
			this.commentService = commentService;
		}

		#endregion

		#region Public Properties

		public BlogConfigurationDto BlogConfiguration
		{
			get
			{
				return this.ConfigurationService.GetConfiguration();
			}
		}

		public ICommentService CommentService
		{
			get
			{
				return this.commentService;
			}
		}

		public IConfigurationService ConfigurationService
		{
			get
			{
				return this.configurationService;
			}
		}

		public ILog Logger
		{
			get
			{
				return this.logger;
			}
		}

		public IPostService PostService
		{
			get
			{
				return this.postService;
			}
		}

		#endregion
	}
}