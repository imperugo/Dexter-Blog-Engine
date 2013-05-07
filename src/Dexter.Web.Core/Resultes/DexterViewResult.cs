namespace Dexter.Web.Core.Resultes
{
	using System.Web.Mvc;

	using Dexter.Dependency;
	using Dexter.Web.Core.Theme;

	public class DexterViewResult : ViewResult
	{
		private readonly IThemeHelper themeHelper;

		public DexterViewResult()
			: this(DexterContainer.Resolve<IThemeHelper>())
		{
		}

		public DexterViewResult(IThemeHelper themeHelper)
		{
			this.themeHelper = themeHelper;
		}

		protected override ViewEngineResult FindView(ControllerContext context)
		{
			return base.FindView(context);
		}
	}
}