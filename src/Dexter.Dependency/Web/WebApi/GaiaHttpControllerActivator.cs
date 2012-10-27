namespace Dexter.Dependency.Web.WebApi
{
	using System;
	using System.Net.Http;
	using System.Web.Http.Controllers;
	using System.Web.Http.Dispatcher;

	using Common.Logging;

	public class DexterHttpControllerActivator : IHttpControllerActivator
	{
		#region Fields

		private readonly IDexterContainer container;

		private readonly ILog logger;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Object" /> class.
		/// </summary>
		public DexterHttpControllerActivator(IDexterContainer container, ILog logger)
		{
			this.container = container;
			this.logger = logger;
		}

		#endregion

		#region Public Methods and Operators

		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			this.logger.InfoFormat("Returning controller {0}", controllerType);

			return (IHttpController)this.container.Resolve(controllerType);
		}

		#endregion
	}
}