namespace Dexter.Host.Areas.Dxt_Admin.Binders
{
	using System;

	public class PageBinder : ItemBinder
	{
		public static PageBinder EmptyInstance()
		{
			return new PageBinder
				       {
					       PublishAt = DateTimeOffset.Now,
					       PublishHour = DateTimeOffset.Now.Hour,
					       PublishMinutes = DateTimeOffset.Now.Minute,
					       AllowComments = true
				       };
		}
	}
}