namespace Dexter.Host.Areas.Dxt_Admin.Binders
{
	using System;

	using Dexter.Host.App_Start.Validators;

	public class PostBinder : ItemBinder
	{
		#region Public Properties

		[SimplyCategoryValidation]
		public string[] Categories { get; set; }

		#endregion

		public static PostBinder EmptyInstance()
		{
			return new PostBinder
			{
				PublishAt = DateTimeOffset.Now,
				PublishHour = DateTimeOffset.Now.Hour,
				PublishMinutes = DateTimeOffset.Now.Minute,
				AllowComments = true
			};
		}
	}
}