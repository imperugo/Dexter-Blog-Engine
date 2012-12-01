namespace Dexter.Navigation.Contracts
{
	using Dexter.Entities;
	using Dexter.Navigation.Helpers;

	public interface IPostUrlBuilder
	{
		SiteUrl Permalink(ItemDto item);
	}
}