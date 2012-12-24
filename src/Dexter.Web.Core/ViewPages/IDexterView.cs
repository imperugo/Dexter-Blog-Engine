#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IDexterView.cs
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
	using Dexter.Navigation.Contracts;

	/// <summary>
	/// 	The base contract for all views
	/// </summary>
	public interface IDexterView
	{
		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether this instance is moderator.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is moderator; otherwise, <c>false</c>.
		/// </value>
		bool IsModerator { get; }

		/// <summary>
		/// 	Return the instance of <see cref = "IUrlBuilder" />.
		/// </summary>
		/// <value>The U.</value>
		IUrlBuilder U { get; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Check if the specified username is the current logged user.
		/// </summary>
		/// <param name = "username">The username.</param>
		bool CurrentuserIsEqualtTo(string username);

		#endregion
	}
}