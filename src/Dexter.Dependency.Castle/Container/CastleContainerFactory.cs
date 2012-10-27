namespace Dexter.Dependency.Castle.Container
{
	using System;
	using System.Web;

	/// <summary>
	/// 	Concrete class to create IoC Engine
	/// </summary>
	public class CastleContainerFactory : IGaiaContainerFactory
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	Creates this instance.
		/// </summary>
		/// <returns> An instance of the Container </returns>
		public IGaiaContainer Create()
		{
			return this.Create(null);
		}

		/// <summary>
		/// 	Creates this instance.
		/// </summary>
		/// <param name="configuration"> The configuration. </param>
		/// <returns> An instance of the Container </returns>
		public IGaiaContainer Create(string configuration)
		{
			CastleContainer castle = new CastleContainer();
			if (!String.IsNullOrEmpty(configuration))
			{
				configuration = HttpContext.Current != null ? HttpContext.Current.Server.MapPath(configuration) : string.Concat(AppDomain.CurrentDomain.BaseDirectory, configuration.Replace("~/", string.Empty));
			}

			castle.Configure(configuration);

			return castle;
		}

		#endregion
	}
}