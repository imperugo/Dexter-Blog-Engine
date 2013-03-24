namespace Dexter.Shared.Exceptions
{
	public class DexterPostNotFoundException : DexterItemNotFoundException
	{
		#region Fields

		private readonly string slug;

		#endregion

		#region Constructors and Destructors

		public DexterPostNotFoundException()
		{
		}

		public DexterPostNotFoundException(int key)
			: base(key)
		{
		}

		public DexterPostNotFoundException(string message, int key)
			: base(message, key)
		{
		}

		public DexterPostNotFoundException(string slug)
		{
			this.slug = slug;
		}

		public DexterPostNotFoundException(string slug, string message)
			: base(message)
		{
			this.slug = slug;
		}

		#endregion

		#region Public Properties

		public string Slug
		{
			get
			{
				return this.slug;
			}
		}

		#endregion
	}
}