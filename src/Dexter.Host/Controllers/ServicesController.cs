#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ServicesController.cs
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

namespace Dexter.Host.Controllers
{
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Services;
	using Dexter.Web.Core.Controllers.Web;

	public class ServicesController : DexterControllerBase
	{
		#region Constructors and Destructors

		public ServicesController(ILog logger, IConfigurationService configurationService, IPostService postService, ICommentService commentService)
			: base(logger, configurationService, postService, commentService)
		{
		}

		#endregion

		#region Public Methods and Operators

		public ActionResult Pingback()
		{
			return null;
		}

		#endregion
	}
}