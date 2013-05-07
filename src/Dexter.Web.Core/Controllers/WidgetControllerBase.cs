#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			WidgetControllerBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/05
// Last edit:	2013/05/06
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Controllers
{
	using System;
	using System.Web.Mvc;

	using Common.Logging;

	using Dexter.Services;
	using Dexter.Web.Core.Resultes;
	using Dexter.Web.Core.Theme;

	public abstract class WidgetControllerBase : DexterControllerBase
	{
		private readonly IThemeHelper themeHelper;

		protected WidgetControllerBase(ILog logger, IConfigurationService configurationService, IThemeHelper themeHelper)
			: base(logger, configurationService)
		{
			this.themeHelper = themeHelper;
		}

		public IThemeHelper ThemeHelper
		{
			get
			{
				return this.themeHelper;
			}
		}

		protected new ViewResult View(object model)
		{
			return this.View(null, null, model);
		}

		protected internal new ViewResult View(string viewName, string masterName, object model)
		{
			if (model != null)
			{
				this.ViewData.Model = model;
			}

			DexterViewResult viewResult = new DexterViewResult();
			viewResult.ViewName = viewName;
			viewResult.MasterName = masterName;
			viewResult.ViewData = this.ViewData;
			viewResult.TempData = this.TempData;
			viewResult.ViewEngineCollection = this.ViewEngineCollection;

			return viewResult;
		}
	}
}