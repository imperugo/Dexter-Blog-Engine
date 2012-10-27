namespace Dexter.Data.Raven.Domain
{
	public class SmtpConfiguration 
	{
		/// <summary>
		/// Gets or sets the SMTP host.
		/// </summary>
		/// <value>The SMTP host.</value>
		public string SmtpHost { get; set; }

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		/// <value>The port.</value>
		public int Port { get; set; }

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>The username.</value>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [use SSL].
		/// </summary>
		/// <value><c>true</c> if [use SSL]; otherwise, <c>false</c>.</value>
		public bool UseSSL { get; set; }
	}
}