namespace Dexter.Navigation.Contracts
{
	using Dexter.Navigation.Helpers;

	public interface IAdminAccountUrlBuilder
	{
		SiteUrl List();

		SiteUrl Delete(string username);
	}
}