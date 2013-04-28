#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ExceptionHandlingAttribute.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/28
// Last edit:	2013/04/28
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Api.Core.Attributes
{
	using System.Net;
	using System.Net.Http;
	using System.Web.Http;
	using System.Web.Http.Filters;

	/// <summary>
	///     Global exception handler
	/// </summary>
	public class ExceptionHandlingAttribute : ExceptionFilterAttribute
	{
		#region Public Methods and Operators

		public override void OnException(HttpActionExecutedContext context)
		{
			throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
				                                {
					                                Content = new StringContent(context.Exception.Message), 
					                                ReasonPhrase = "Internal server exception"
				                                });
		}

		#endregion
	}
}