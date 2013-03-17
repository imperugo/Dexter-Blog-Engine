namespace Dexter.Shared.Exceptions
{
	public class DexterPageNotFoundException : DexterItemNotFoundException
	{
		#region Fields

		private readonly string slug;

		#endregion

		#region Constructors and Destructors

		public DexterPageNotFoundException(int key)
			: base(key)
		{
		}

		public DexterPageNotFoundException(string message, int key)
			: base(message, key)
		{
		}

		protected DexterPageNotFoundException(string slug)
		{
			this.slug = slug;
		}

		protected DexterPageNotFoundException(string slug, string message)
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