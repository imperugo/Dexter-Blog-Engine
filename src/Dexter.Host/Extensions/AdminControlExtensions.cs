#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AdminControlExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/02
// Last edit:	2012/12/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Extensions
{
	using System;
	using System.Linq.Expressions;
	using System.Web.Mvc;
	using System.Web.Mvc.Html;

	public static class AdminControlExtensions
	{
		#region Public Methods and Operators

		public static MvcHtmlString DexterValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			TagBuilder containerDivBuilder = new TagBuilder("div");
			containerDivBuilder.AddCssClass("field-error-box");

			TagBuilder topDivBuilder = new TagBuilder("span");
			topDivBuilder.AddCssClass("check-error");

			containerDivBuilder.InnerHtml += topDivBuilder.ToString(TagRenderMode.Normal);

			var errorBox = MvcHtmlString.Create(containerDivBuilder.ToString(TagRenderMode.Normal));

			return ValidationExtensions.ValidationMessageFor(helper, expression,errorBox.ToHtmlString());
		}

		#endregion
	}
}