namespace Dexter.Navigation.Contracts
{
	using Dexter.Navigation.Helpers;

	public interface IAdminCategoryUrlBuilder
	{
		#region Public Methods and Operators

		SiteUrl List();

		SiteUrl New();

		#endregion
	}
}