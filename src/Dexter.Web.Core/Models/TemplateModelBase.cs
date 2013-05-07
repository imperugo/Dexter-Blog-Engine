#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TemplateModelBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/05/07
// Last edit:	2013/05/07
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Models
{
	using System.Dynamic;

	public class TemplateModelBase : DynamicObject
	{
		protected TemplateModelBase()
			: this("../Shared/_layout.cshtml")
		{
		}

		protected TemplateModelBase(string template)
		{
			this.Template = template;
		}

		public string Template { get; set; }
	}
}