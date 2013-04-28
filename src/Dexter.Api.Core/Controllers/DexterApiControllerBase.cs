#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterApiControllerBase.cs
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

namespace Dexter.Api.Core.Controllers
{
	using System.Net;
	using System.Net.Http;
	using System.Web.Http;

	using Common.Logging;

	using Dexter.Entities;
	using Dexter.Services;

	public class DexterApiControllerBase : ApiController
	{
		#region Fields

		private readonly IConfigurationService configurationService;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		public DexterApiControllerBase(ILog logger, IConfigurationService configurationService)
		{
			this.logger = logger;
			this.configurationService = configurationService;
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

		#endregion

		/// <summary>
		/// Raises an invalid parameter exception.
		/// </summary>
		/// <param name="parameterName">Name of the invalid parameter.</param>
		/// <exception cref="System.Web.Http.HttpResponseException"></exception>
		protected void RaiseInvalidParameterException(string parameterName)
		{
			string errorMessage = string.Format("Parameter: {0} is not valid or contains and invalid value", parameterName);
			throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage));
		}

		protected void RaiseNotFoundException(string message)
		{
			throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
		}
	}
}