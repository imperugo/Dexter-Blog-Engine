#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterCallContextFactory.cs
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

namespace Dexter.Async
{
	using System.Web;

	using Dexter.Async.Async;
	using Dexter.Async.Web;

	public class DexterCallContextFactory : ICallContextFactory
	{
		#region Fields

		private readonly ICallContext contextAsync;

		private readonly ICallContext contextWeb;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DexterCallContextFactory"/> class.
		/// </summary>
		/// <param name="contextWeb">The context web.</param>
		/// <param name="contextAsync">The context async.</param>
		public DexterCallContextFactory(IWebCallContext contextWeb, IAsyncCallContext contextAsync)
		{
			this.contextWeb = contextWeb;
			this.contextAsync = contextAsync;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Determines whether [is web request].
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if [is web request]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsWebRequest
		{
			get
			{
				return HttpContext.Current != null;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Return the WebCallContext or the ThreadCallContext
		/// </summary>
		/// <returns></returns>
		public ICallContext RetrieveCallContext()
		{
			return !this.IsWebRequest
				       ? this.contextAsync
				       : this.contextWeb;
		}

		#endregion
	}
}