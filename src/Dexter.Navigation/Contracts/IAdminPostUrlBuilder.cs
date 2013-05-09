namespace Dexter.Navigation.Contracts
{
	using Dexter.Shared.Dto;
	using Dexter.Navigation.Helpers;

	public interface IAdminPostUrlBuilder
	{
		#region Public Methods and Operators

		SiteUrl Delete(ItemDto item);

		SiteUrl Edit(ItemDto item);

		SiteUrl List();

		SiteUrl New();

		#endregion
	}
}