#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SyndicationController.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/28
// Last edit:	2013/04/28
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Api
{
	using System.Net;
	using System.Net.Http;
	using System.Web.Http;
	using System.Xml;
	using System.Xml.Linq;

	using Common.Logging;

	using Dexter.Api.Core.Controllers;
	using Dexter.Api.Core.Formatters.Syndication;
	using Dexter.Entities;
	using Dexter.Entities.Result;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;

	public class SyndicationController : DexterApiControllerBase
	{
		#region Fields

		private readonly IPostService postService;

		private readonly IUrlBuilder urlBuilder;

		#endregion

		#region Constructors and Destructors

		public SyndicationController(ILog logger, IConfigurationService configurationService, IPostService postService, IUrlBuilder urlBuilder)
			: base(logger, configurationService)
		{
			this.postService = postService;
			this.urlBuilder = urlBuilder;
		}

		#endregion

		#region Public Methods and Operators

		[HttpGet]
		public HttpResponseMessage ByCategory(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				this.RaiseNotFoundException("Invalid category name");
			}

			IPagedResult<PostDto> posts = this.postService.GetPostsByCategory(1, this.BlogConfiguration.ReadingConfiguration.NumberOfPostPerFeed, id);

			return this.Request.CreateResponse(
				HttpStatusCode.OK, 
				posts.Result, 
				new SyndicationFeedFormatter());
		}

		[HttpGet]
		public HttpResponseMessage ByTag(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				this.RaiseNotFoundException("Invalid tag name");
			}

			IPagedResult<PostDto> posts = this.postService.GetPostsByTag(1, this.BlogConfiguration.ReadingConfiguration.NumberOfPostPerFeed, id);

			return this.Request.CreateResponse(
				HttpStatusCode.OK, 
				posts.Result, 
				new SyndicationFeedFormatter());
		}

		[HttpGet]
		public HttpResponseMessage MainFeed()
		{
			IPagedResult<PostDto> posts = this.postService.GetPosts(1, this.BlogConfiguration.ReadingConfiguration.NumberOfPostPerFeed);

			return this.Request.CreateResponse(
				HttpStatusCode.OK, 
				posts.Result, 
				new SyndicationFeedFormatter());
		}

		[HttpGet]
		public HttpResponseMessage Rsd()
		{
			XNamespace ns = XNamespace.Get("http://archipelago.phrasewise.com/rsd");
			XDocument response = new XDocument(
				new XElement(ns + "service", 
					new XElement(ns + "engineName", "Dexter Blog Engine"), 
					new XElement(ns + "engineLink", "http://www.dexterblogengine.com"), 
					new XElement(ns + "homePageLink", this.urlBuilder.Home), 
					new XElement(ns + "apis", 
						new XElement(ns + "api", 
							new XAttribute("name", "MetaWeblog"), 
							new XAttribute("preferred", "true"), 
							new XAttribute("blogID", "0"), 
							new XAttribute("apiLink", this.urlBuilder.Service.MetaWebLogApi())))));

			return new HttpResponseMessage
				       {
					       Content = new XmlContent(response.ToXmlDocument())
				       };
		}

		#endregion
	}
}