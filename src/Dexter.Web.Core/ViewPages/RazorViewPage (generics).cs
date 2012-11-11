#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			RazorViewPage (generics).cs
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

namespace Dexter.Web.Core.ViewPages
{
	using System.Collections.Generic;
	using System.Web.Mvc;

	using Dexter.Dependency;
	using Dexter.Navigation.Contracts;

	public abstract class RazorViewPage<TModel> : RazorView, IDexterView
		where TModel : class
	{
		#region Fields

		private readonly ControllerContext contex;

		private IUrlBuilder urlBuilder;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref = "RazorViewPage&lt;TModel&gt;" /> class.
		/// </summary>
		/// <param name = "controllerContext">The controller context.</param>
		/// <param name = "viewPath">The view path.</param>
		/// <param name = "layoutPath">The layout path.</param>
		/// <param name = "runViewStartPages">if set to <c>true</c> [run view start pages].</param>
		/// <param name = "viewStartFileExtensions">The view start file extensions.</param>
		protected RazorViewPage(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions)
			: base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions)
		{
			this.contex = controllerContext;
			this.CreateHelpers();
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref = "RazorViewPage&lt;TModel&gt;" /> class.
		/// </summary>
		/// <param name = "controllerContext">The controller context.</param>
		/// <param name = "viewPath">The view path.</param>
		/// <param name = "layoutPath">The layout or master page.</param>
		/// <param name = "runViewStartPages">A value that indicates whether view start files should be executed before the view.</param>
		/// <param name = "viewStartFileExtensions">The set of extensions that will be used when looking up view start files.</param>
		/// <param name = "viewPageActivator">The view page activator.</param>
		protected RazorViewPage(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions, IViewPageActivator viewPageActivator)
			: base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, viewPageActivator)
		{
			this.contex = controllerContext;
			this.CreateHelpers();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// 	Gets a value indicating whether this instance is moderator.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is moderator; otherwise, <c>false</c>.
		/// </value>
		public bool IsModerator
		{
			get
			{
				return this.contex.HttpContext.User.IsInRole("Moderator");
			}
		}

		/// <summary>
		/// 	Return the instance of <see cref = "IUrlBuilder" />.
		/// </summary>
		/// <value>The U.</value>
		public IUrlBuilder U
		{
			get
			{
				return this.urlBuilder;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Check if the specified username is the current logged user.
		/// </summary>
		/// <param name = "username">The username.</param>
		/// <returns></returns>
		public bool CurrentuserIsEqualtTo(string username)
		{
			return this.contex.HttpContext.User.Identity.Name == username;
		}

		#endregion

		#region Methods

		/// <summary>
		/// 	Creates the helpers.
		/// </summary>
		protected void CreateHelpers()
		{
			this.urlBuilder = DexterContainer.Resolve<IUrlBuilder>();
		}

		#endregion
	}
}