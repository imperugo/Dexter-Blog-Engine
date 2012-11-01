#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterControllerActionInvoker.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Invokers
{
	using System.Web.Mvc;

	using Dexter.Services;
	using Dexter.Web.Core.Models;

	public class DexterControllerBase : ControllerActionInvoker
	{
		private readonly IPostService postService;

		public DexterControllerBase(IPostService postService)
		{
			this.postService = postService;
		}

		#region Methods

		/// <summary>
		/// Creates the action result.
		/// </summary>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="actionDescriptor">The action descriptor.</param>
		/// <param name="actionReturnValue">The action return value.</param>
		/// <returns>
		/// The action result object.
		/// </returns>
		protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
		{
			if (actionReturnValue == null)
			{
				return new EmptyResult();
			}

			ViewResultBase result = actionReturnValue as ViewResultBase;

			if (result != null && result.Model is DexterModelBase)
			{
				DexterModelBase model = (DexterModelBase)result.Model;

				//DexterControllerBase controllerBase = ((DexterControllerBase)controllerContext.Controller);

				model.RecentPost = this.postService.GetPosts(1, 5, null).Result;
			}

			return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
		}

		#endregion
	}
}