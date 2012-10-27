namespace Dexter.Host.Areas.Dxt_Admin
{
	using System.Web.Mvc;

	public class Dxt_AdminAreaRegistration : AreaRegistration
	{
		#region Public Properties

		public override string AreaName
		{
			get
			{
				return "Dxt-Admin";
			}
		}

		#endregion

		#region Public Methods and Operators

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute("Dxt-Admin_default", "Dxt-Admin/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
		}

		#endregion
	}
}