namespace Dexter.Automapper.Extensions.Resolvers
{
	using System;

	using AutoMapper;

	public class UriToStringTypeConverter : TypeConverter<Uri, string>
	{
		#region Methods

		protected override string ConvertCore(Uri source)
		{
			if (source == null)
			{
				return null;
			}

			return source.ToString();
		}

		#endregion
	}
}