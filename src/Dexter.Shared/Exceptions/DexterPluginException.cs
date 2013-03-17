namespace Dexter.Shared.Exceptions
{
	public class DexterPluginException : DexterException
	{
		#region Fields

		private readonly string plugName;

		#endregion

		#region Constructors and Destructors

		public DexterPluginException(string message, string plugName)
			: base(message)
		{
			this.plugName = plugName;
		}

		public DexterPluginException(string plugName)
		{
			this.plugName = plugName;
		}

		#endregion

		#region Public Properties

		public string PlugName
		{
			get
			{
				return this.plugName;
			}
		}

		#endregion
	}
}