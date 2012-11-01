#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IDexterApplication.cs
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

namespace Dexter.Web.Core.HttpApplication
{
	using System.Web;

	public interface IDexterApplication
	{
		#region Public Methods and Operators

		void ApplicationEnd();

		void ApplicationError(HttpApplication application);

		void ApplicationStart();

		void AuthenticateRequest();

		void Init(HttpApplication application);

		#endregion
	}
}