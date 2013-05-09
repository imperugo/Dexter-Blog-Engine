#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterControllerBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/01
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Web.Core.Controllers
{
	using System.Web.Mvc;
	using System.Xml.Linq;

	using Common.Logging;

	using Dexter.Shared.Dto;
	using Dexter.Services;
	using Dexter.Web.Core.Resultes;

	public abstract class DexterControllerBase : AsyncController
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

		protected ActionResult Xml(XDocument xml)
		{
			return this.Xml(xml, null);
		}

		protected ActionResult Xml(XDocument xml, string etag)
		{
			return new XmlResult(xml, etag);
		}

		#endregion
	}
}