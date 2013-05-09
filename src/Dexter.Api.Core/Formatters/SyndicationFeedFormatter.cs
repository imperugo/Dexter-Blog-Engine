#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			SyndicationFeedFormatter.cs
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

namespace Dexter.Api.Core.Formatters.Syndication
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Net.Http.Formatting;
	using System.Net.Http.Headers;
	using System.ServiceModel.Syndication;
	using System.Threading.Tasks;
	using System.Xml;

	using Dexter.Dependency;
	using Dexter.Dependency.Extensions;
	using Dexter.Shared.Dto;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;

	public class SyndicationFeedFormatter : MediaTypeFormatter
	{
		#region Constants

		private const string Atom = "application/atom+xml";

		private const string Rss = "application/rss+xml";

		#endregion

		#region Fields

		private readonly Func<Type, bool> supportedType =
			type =>
				{
					if (type == typeof(PostDto) || type == typeof(IEnumerable<PostDto>))
					{
						return true;
					}

					return false;
				};

		private IUrlBuilder urlBuilder;

		#endregion

		#region Constructors and Destructors

		public SyndicationFeedFormatter()
		{
			this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(Atom));
			this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(Rss));
		}

		#endregion

		#region Properties

		private IUrlBuilder UrlBuilder
		{
			get
			{
				return this.urlBuilder ?? (this.urlBuilder = DexterContainer.Resolve<IUrlBuilder>());
			}
		}

		#endregion

		#region Public Methods and Operators

		public override bool CanReadType(Type type)
		{
			return this.supportedType(type);
		}

		public override bool CanWriteType(Type type)
		{
			return this.supportedType(type);
		}

		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return Task.Factory.StartNew(() =>
				{
					if (type == typeof(PostDto) || type == typeof(IEnumerable<PostDto>))
					{
						this.BuildSyndicationFeed(value, writeStream, content.Headers.ContentType.MediaType);
					}
				});
		}

		#endregion

		#region Methods

		private void BuildSyndicationFeed(object models, Stream stream, string contenttype)
		{
			List<PostDto> urls = new List<PostDto>();

			if (models is IEnumerable<ItemDto>)
			{
				urls.AddRange((IEnumerable<PostDto>)models);
			}
			else
			{
				urls.Add((PostDto)models);
			}

			BlogConfigurationDto config = DexterContainer.Resolve<IConfigurationService>().GetConfiguration();

			//Main info
			SyndicationFeed feed = new SyndicationFeed
				                       {
					                       Title = new TextSyndicationContent(config.Name), 
					                       Description = new TextSyndicationContent(config.SeoConfiguration.DefaultDescription ?? config.Name), 
					                       Copyright = new TextSyndicationContent(String.Format("{0} (c) {1}", config.SeoConfiguration.CopyRight, DateTime.Now.Year))
				                       };

			//Adding link
			feed.Links.Add(new SyndicationLink(this.UrlBuilder.Home));

			//Adding categoris
			config.SeoConfiguration.DefaultKeyWords.ForEach(keyword => feed.Categories.Add(new SyndicationCategory(keyword)));

			if (urls.Any())
			{
				feed.LastUpdatedTime = urls.OrderByDescending(model => model.PublishAt).First().PublishAt;
				
				//Adding authors
				urls.GroupBy(x => x.Author).Select(x => x.Key)
			    .ForEach(author => feed.Authors.Add(new SyndicationPerson(null, author.Username, this.UrlBuilder.Home)));
			} 

			//Adding items
			List<SyndicationItem> items = new List<SyndicationItem>();
			urls.ForEach(item => items.Add(this.BuildSyndicationItem(item)));

			feed.Items = items;

			using (XmlWriter writer = XmlWriter.Create(stream))
			{
				if (string.Equals(contenttype, Atom))
				{
					Atom10FeedFormatter atomformatter = new Atom10FeedFormatter(feed);
					atomformatter.WriteTo(writer);
				}
				else
				{
					Rss20FeedFormatter rssformatter = new Rss20FeedFormatter(feed);
					rssformatter.WriteTo(writer);
				}
			}
		}

		private SyndicationItem BuildSyndicationItem(PostDto item)
		{
			SyndicationItem feedItem = new SyndicationItem
				                           {
					                           Title = new TextSyndicationContent(item.Title), 
					                           BaseUri = this.UrlBuilder.Post.Permalink(item), 
					                           LastUpdatedTime = item.PublishAt, 
					                           Content = new TextSyndicationContent(item.Content), 
					                           PublishDate = item.PublishAt
				                           };
			feedItem.Authors.Add(new SyndicationPerson
				                     {
					                     Name = item.Author.Username
				                     });

			return feedItem;
		}

		#endregion
	}
}