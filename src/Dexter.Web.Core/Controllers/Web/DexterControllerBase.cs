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


		private readonly IConfigurationService configurationService;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		public DexterControllerBase(ILog logger, IConfigurationService configurationService)
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
	}
}