#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			WebViewPage (generic).cs
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

namespace Dexter.Web.Core.ViewPages
{
	using Dexter.Dependency;
	using Dexter.Navigation.Contracts;

	/// <summary>
	/// 	Represents the properties and methods that are needed in order to render a view that uses ASP.NET Razor syntax.
	/// </summary>
	/// <typeparam name = "TModel">The type of the model.</typeparam>
	public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>, IDexterView
	{
		#region Fields

		private IUrlBuilder urlBuilder;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether this instance is moderator.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is moderator; otherwise, <c>false</c>.
		/// </value>
		public bool IsModerator
		{
			get
			{
				return this.Context.User.IsInRole("Moderator");
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
			return this.Context.User.Identity.Name == username;
		}

		public override void InitHelpers()
		{
			base.InitHelpers();

			this.urlBuilder = DexterContainer.Resolve<IUrlBuilder>();
		}

		#endregion
	}
}