#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			MetaWeblogHandler.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/17
// Last edit:	2013/03/17
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.HttpHandlers
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Security.Principal;
	using System.Threading;
	using System.Web;
	using System.Web.Security;

	using Common.Logging;

	using CookComputing.XmlRpc;

	using Dexter.Dependency;
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Navigation.Contracts;
	using Dexter.Services;
	using Dexter.Shared;
	using Dexter.Shared.Exceptions;
	using Dexter.Shared.Helpers;
	using Dexter.Web.Core.MetaWeblogApi;
	using Dexter.Web.Core.MetaWeblogApi.Domain;
	using Dexter.Web.Core.Routing;

	public class MetaWeblogHandler : XmlRpcService, IMetaWeblog
	{
		#region Fields

		private readonly ICategoryService categoryService;

		private readonly IConfigurationService configurationService;

		private readonly ILog logger = LogManager.GetCurrentClassLogger();

		private readonly IPageService pageService;

		private readonly IPostService postService;

		private readonly IRoutingService routingService;

		private readonly IUrlBuilder urlBuilder;

		#endregion

		#region Constructors and Destructors

		public MetaWeblogHandler()
		{
			this.categoryService = DexterContainer.Resolve<ICategoryService>();
			this.configurationService = DexterContainer.Resolve<IConfigurationService>();
			this.pageService = DexterContainer.Resolve<IPageService>();
			this.postService = DexterContainer.Resolve<IPostService>();
			this.routingService = DexterContainer.Resolve<IRoutingService>();
			this.urlBuilder = DexterContainer.Resolve<IUrlBuilder>();
		}

		#endregion

		#region Public Methods and Operators

		public string AddPost(string blogid, string username, string password, Post post, bool publish)
		{
			this.ValidateUser(username, password);

			string data = this.ProcessPostData(blogid, username, password, post, null);

			return data;
		}

		public bool DeletePost(string key, string postid, string username, string password, bool publish)
		{
			this.ValidateUser(username, password);

			this.postService.Delete(postid.ToInt32(0));

			// todo: what to do with the media object related to the post we are removing ?

			return true;
		}

		public CategoryInfo[] GetCategories(string blogid, string username, string password)
		{
			this.ValidateUser(username, password);

			IList<CategoryDto> categories = this.categoryService.GetCategories();

			CategoryInfo[] data = categories.Select(c => new CategoryInfo
				                                             {
					                                             categoryid = c.Id.ToString(), 
					                                             parentid = c.Parent == null
						                                                        ? String.Empty
						                                                        : c.Parent.Id.ToString(CultureInfo.InvariantCulture), 
					                                             title = c.Name, 
					                                             description = string.IsNullOrEmpty(c.Description)
						                                                           ? string.Empty
						                                                           : c.Name, 
					                                             
					                                             //TODO: Da implementare l'url builder
					                                             htmlUrl = string.Empty, 
					                                             rssUrl = string.Empty, 
				                                             }).ToArray();

			return data;
		}

		public Post GetPost(string postid, string username, string password)
		{
			this.ValidateUser(username, password);

			PostDto item = this.postService.GetPostByKey(postid.ToInt32());

			WpAuthor[] authors = this.WpGetAuthors(string.Empty, username, password);
			Post p = this.GetMetaweblogPost(item, authors);

			return p;
		}

		public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
		{
			this.ValidateUser(username, password);

			IPagedResult<PostDto> posts = this.postService.GetPosts(1, numberOfPosts, new ItemQueryFilter
				                                                                          {
					                                                                          MaxPublishAt = DateTimeOffset.Now.AddYears(1), 
					                                                                          MinPublishAt = DateTimeOffset.Now.AddYears(-10)
				                                                                          });

			Post[] items = new Post[posts.Result.Count()];

			WpAuthor[] authors = this.WpGetAuthors(string.Empty, username, password);

			int i = 0;
			foreach (PostDto post in posts.Result)
			{
				items[i] = this.GetMetaweblogPost(post, authors);
				i++;
			}

			return items;
		}

		public UserInfo GetUserInfo(string key, string username, string password)
		{
			this.ValidateUser(username, password);

			UserInfo info = new UserInfo();
			MembershipUser user = Membership.GetUser(username);

			if (user == null)
			{
				throw new DexterSecurityException(string.Format("Unable to locate the user {0}", username));
			}

			info.email = user.Email;
			info.nickname = username;
			info.url = this.urlBuilder.Home;
			info.userid = username;

			return info;
		}

		public BlogInfo[] GetUsersBlogs(string key, string username, string password)
		{
			this.ValidateUser(username, password);

			BlogConfigurationDto conf = this.configurationService.GetConfiguration();

			BlogInfo blogInfo = new BlogInfo
				                    {
					                    blogid = "nothing", 
					                    blogName = conf.Name, 
					                    url = this.urlBuilder.Home, 
				                    };

			return new[] { blogInfo };
		}

		public MtCategory[] MtGetPostCategories(string postid, string username, string password)
		{
			this.ValidateUser(username, password);

			PostDto item = this.postService.GetPostByKey(postid.ToInt32());

			IList<CategoryDto> categories = this.categoryService.GetCategories();

			MtCategory[] cats = new MtCategory[item.Categories.Length];
			for (int i = 0; i < item.Categories.Length; i++)
			{
				CategoryDto cat = categories.Single(c => c.Name.ToLowerInvariant() == item.Categories[i]);
				cats[i] = new MtCategory
					          {
						          categoryId = cat.Id.ToString(CultureInfo.InvariantCulture), 
						          categoryName = cat.Name, 
						          
						          // todo: fill up the 'is primary field", maybe this must be true for the first category only
						          isPrimary = (i == 0)
					          };
			}

			return cats;
		}

		public bool MtSetPostCategories(string postid, string username, string password, MtCategory[] categories)
		{
			this.ValidateUser(username, password);

			// do nothing here, we handle the category assignemnt in the AddPost and UpdatePost functions
			return true;
		}

		public MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
		{
			this.ValidateUser(username, password);

			string userFolderName = username.MakeValidFileName();

			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("UserFiles\\{0}\\{1}", userFolderName, mediaObject.name));

			//GetFileName(mediaObject.name,targetFolder);

			if (mediaObject.bits != null)
			{
				using (MemoryStream ms = new MemoryStream(mediaObject.bits))
				{
					using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
					{
						byte[] bytes = new byte[ms.Length];
						ms.Read(bytes, 0, (int)ms.Length);
						file.Write(bytes, 0, bytes.Length);
						file.Close();
					}
					ms.Close();
				}

				MediaObjectInfo objectInfo = new MediaObjectInfo
					                             {
						                             url = this.urlBuilder.ResolveUrl(string.Format("~/UserFiles/{0}/{1}", userFolderName, mediaObject.name))
					                             };

				return objectInfo;
			}

			throw new XmlRpcFaultException(0, "Invalid Media");
		}

		public bool UpdatePost(string postid, string username, string password, Post post, bool publish)
		{
			this.ValidateUser(username, password);

			this.ProcessPostData(string.Empty, username, password, post, postid.ToInt32(0));

			return true;
		}

		public WpAuthor[] WpGetAuthors(string blog_id, string username, string password)
		{
			this.ValidateUser(username, password);

			string[] usrs = Roles.GetUsersInRole(Constants.Editor);

			WpAuthor[] authors = new WpAuthor[usrs.Length];

			for (int i = 0; i < usrs.Length; i++)
			{
				MembershipUser user = Membership.GetUser(usrs[i]);

				authors[i] = new WpAuthor
					             {
						             display_name = usrs[i], 
						             user_login = usrs[i], 
						             user_email = user.Email, 
						             meta_value = string.Empty
					             };
			}

			return authors;
		}

		public WpCategoryInfo[] WpGetCategories(string blogid, string username, string password)
		{
			throw new NotImplementedException();
		}

		public WpTagInfo[] WpGetTags(string blog_id, string username, string password)
		{
			this.ValidateUser(username, password);

			IList<TagDto> tags = this.postService.GetTopTagsForPublishedPosts(500);
			WpTagInfo[] tagArray = tags.Select(o => new WpTagInfo
				                                        {
					                                        name = o.Name, 
					                                        count = o.Count, 
					                                        slug = o.Name, 
					                                        html_url = string.Empty, 
					                                        rss_url = string.Empty
				                                        }).ToArray();

			return tagArray;
		}

		public string WpNewCategory(string blog_id, string username, string password, WpNewCategory category)
		{
			this.ValidateUser(username, password);

			IList<CategoryDto> categories = this.categoryService.GetCategories();

			CategoryDto cat = categories.FirstOrDefault(x => x.Name.ToLowerInvariant() == category.name.ToLowerInvariant());

			CategoryDto parentCategory = null;
			if (category.parent_id > 0)
			{
				parentCategory = categories.FirstOrDefault(c => c.Parent.Id == category.parent_id);
			}

			if (cat == null)
			{
				cat = new CategoryDto
					      {
						      Name = category.name, 
						      Parent = parentCategory
					      };
				this.categoryService.SaveOrUpdate(cat);
			}
			else
			{
				if (cat.Parent != null && cat.Parent.Id != parentCategory.Id)
				{
					cat.Parent.Id = parentCategory.Id;
					this.categoryService.SaveOrUpdate(cat);
				}
			}

			return cat.Id.ToString(CultureInfo.InvariantCulture);
		}

		public bool deletePage(string blog_id, string username, string password, string page_id)
		{
			this.ValidateUser(username, password);

			this.pageService.Delete(page_id.ToInt32(0));

			this.routingService.UpdateRoutes();

			return true;
		}

		public int editPage(string blog_id, string page_id, string username, string password, Page content, bool publish)
		{
			this.ValidateUser(username, password);

			int data = this.ProcessPageData(blog_id, username, password, content, page_id.ToInt32(0));

			return data;
		}

		public Page getPage(string blog_id, string page_id, string username, string password)
		{
			this.ValidateUser(username, password);

			PageDto item = this.pageService.GetPageByKey(page_id.ToInt32());
			WpAuthor[] authors = this.WpGetAuthors(blog_id, username, password);
			Page p = this.GetMetaweblogPage(item, authors);

			return p;
		}

		public PageInfo[] getPageList(string blog_id, string username, string password)
		{
			this.ValidateUser(username, password);

			IEnumerable<PageDto> pages = this.pageService.GetPages(1, int.MaxValue, new ItemQueryFilter
				                                                                        {
					                                                                        MaxPublishAt = DateTimeOffset.Now.AddYears(1), 
					                                                                        MinPublishAt = DateTimeOffset.Now.AddYears(-10), 
				                                                                        }).Result;

			PageInfo[] data = pages.Select(p => new PageInfo
				                                    {
					                                    dateCreated = p.PublishAt.DateTime, 
					                                    page_id = p.Id, 
					                                    
					                                    
					                                    //page_parent_id = p.Parent != null ? p.Parent.Id : 0,
					                                    page_title = p.Title, 
				                                    }).ToArray();

			return data;
		}

		public Page[] getPages(string blog_id, string username, string password, int number)
		{
			this.ValidateUser(username, password);

			IEnumerable<PageDto> pages = this.pageService.GetPages(1, int.MaxValue, new ItemQueryFilter
				                                                                        {
					                                                                        MaxPublishAt = DateTimeOffset.Now.AddYears(1), 
					                                                                        MinPublishAt = DateTimeOffset.Now.AddYears(-10), 
				                                                                        }).Result;
			WpAuthor[] authors = this.WpGetAuthors(blog_id, username, password);

			Page[] data = pages.Select(p => this.GetMetaweblogPage(p, authors)).ToArray();

			return data;
		}

		public int newPage(string blog_id, string username, string password, Page content, bool publish)
		{
			this.ValidateUser(username, password);

			int data = this.ProcessPageData(blog_id, username, password, content, null);

			return data;
		}

		#endregion

		#region Methods

		private void ConvertAllowCommentsForItem(int allowComments, ItemDto item)
		{
			// SiteConfiguration conf;

			switch (allowComments)
			{
				case 0:

					// none and default seem to have the same value...so I return the default
					// and comments are enabled by default on the posts, the moderation shuould be another thing
					item.AllowComments = true;
					break;
				case 1:
					item.AllowComments = true;
					break;
				case 2:
					item.AllowComments = false;
					break;
				default:

					// comments are enabled by default on posts
					item.AllowComments = true;
					break;
			}
		}

		private string[] ExtractTags(Post post)
		{
			string[] tagsFromBody = TagHelper.RetrieveTagsFromBody(post.description);
			string[] tagsPosed = !string.IsNullOrEmpty(post.mt_keywords)
				                     ? post.mt_keywords.Split(new[]
					                                              {
						                                              ';', ','
					                                              })
				                     : new string[] { };
			string[] tags = new string[tagsFromBody.Length + tagsPosed.Length];
			tagsFromBody.CopyTo(tags, 0);
			tagsPosed.CopyTo(tags, tagsFromBody.Length);
			return tags;
		}

		private string GetAuthorId(ItemDto item, IEnumerable<WpAuthor> authors)
		{
			string authorId = null;

			// take into account a deleted author
			WpAuthor auth = authors.FirstOrDefault(a => a.user_login == item.Author);
			if (auth.user_id > 0)
			{
				authorId = auth.user_id.ToString();
			}

			return authorId;
		}

		private string GetFileName(string proposedName, string targetFolder)
		{
			string extension = Path.GetExtension(proposedName);
			string baseName = Path.GetFileNameWithoutExtension(proposedName);
			string name = baseName + extension;
			string filepath = Path.Combine(targetFolder, name);
			int i = 1;
			while (File.Exists(filepath))
			{
				name = string.Format("{0}_{1}{2}", baseName, i, extension);
				filepath = Path.Combine(targetFolder, name);
				i += 1;
			}
			return name;
		}

		private Page GetMetaweblogPage(PageDto p, IEnumerable<WpAuthor> authors)
		{
			string authorId = this.GetAuthorId(p, authors);

			return new Page
				       {
					       dateCreated = p.CreatedAt.DateTime.ToUniversalTime(), 
					       user_id = authorId, 
					       page_id = p.Id, 
					       page_status = p.Status.ToString(), 
					       description = p.Content, 
					       title = p.Title, 
					       link = this.urlBuilder.Page.Permalink(p), 
					       permalink = this.urlBuilder.Page.Permalink(p), 
					       categories = null, 
					       excerpt = p.Excerpt, 
					       text_more = string.Empty, 
					       mt_allow_comments = p.AllowComments
						                           ? 1
						                           : 2, 
					       mt_allow_pings = 0, 
					       wp_slug = p.Slug, 
					       wp_password = string.Empty, 
					       wp_author = p.Author, 
					       
					       
					       
					       
					       
					       
					       
					       
					       
					       //wp_page_parent_id = p.Parent != null
					       //						? p.Parent.Id.ToString()
					       //						: string.Empty,
					       //wp_page_parent_title = p.Parent != null
					       //						? p.Parent.Title
					       //						: string.Empty,
					       //wp_page_order = p.SortOrder.ToString(),
					       wp_author_id = authorId, 
					       wp_author_display_name = p.Author, 
					       date_created_gmt = p.PublishAt.DateTime.ToUniversalTime(), 
					       wp_page_template = string.Empty
				       };
		}

		private Post GetMetaweblogPost(PostDto item, IEnumerable<WpAuthor> authors)
		{
			string authorId = this.GetAuthorId(item, authors);

			// the fields reported here are all the ones you should use
			Post p = new Post
				         {
					         dateCreated = item.CreatedAt.DateTime.ToUniversalTime(), 
					         userid = authorId, 
					         postid = item.Id.ToString(), 
					         description = item.Content, 
					         title = item.Title, 
					         link = this.urlBuilder.Post.Permalink(item), 
					         permalink = this.urlBuilder.Post.Permalink(item), 
					         categories = item.Categories.ToArray(), 
					         mt_excerpt = item.Excerpt, 
					         mt_text_more = string.Empty, 
					         mt_allow_comments = item.AllowComments
						                             ? 1
						                             : 2, // open or close
					         mt_allow_pings = 1, 
					         mt_keywords = string.Join(",", item.Tags), 
					         wp_slug = item.Slug, 
					         wp_password = string.Empty, 
					         wp_author_id = authorId, 
					         wp_author_display_name = item.Author, 
					         date_created_gmt = item.CreatedAt.DateTime.ToUniversalTime(), 
					         post_status = item.Status.ToString(), 
					         custom_fields = null, 
					         sticky = false
				         };
			return p;
		}

		private int ProcessPageData(string blog_id, string username, string password, Page content, int? pageId)
		{
			string body = content.description;
			string title = HttpUtility.HtmlDecode(content.title);

			PageDto newPage = !pageId.HasValue || pageId.Value < 1
				                  ? new PageDto()
				                  : this.pageService.GetPageByKey(pageId.Value);

			// everything that pass through the metaweblog handler goes published by default
			newPage.Status = ItemStatus.Published;
			newPage.Title = title;
			newPage.Slug = content.wp_slug;
			newPage.Content = body;
			newPage.Excerpt = (!string.IsNullOrEmpty(content.description))
				                  ? content.description.CleanHtmlText().Trim().Replace("&nbsp;", string.Empty).Cut(250)
				                  : string.Empty;
			if (newPage.IsTransient)
			{
				newPage.PublishAt = (content.dateCreated == DateTime.MinValue || content.dateCreated == DateTime.MaxValue)
					                    ? DateTimeOffset.Now
					                    : new DateTime(content.dateCreated.Ticks, DateTimeKind.Utc);
			}
			else
			{
				newPage.PublishAt = (content.dateCreated == DateTime.MinValue || content.dateCreated == DateTime.MaxValue)
					                    ? newPage.PublishAt
					                    : new DateTime(content.dateCreated.Ticks, DateTimeKind.Utc);
			}

			//TODO: add parent page

			//int order;
			//int.TryParse(content.wp_page_order, out order);
			////nwPage.SortOrder = order;

			//int parentId;
			//if (int.TryParse(content.wp_page_parent_id, out parentId))
			//{
			//	if (parentId > 0)
			//	{
			//		var parentPage = pageService.GetPageByKey(parentId);
			//		nwPage.Parent = parentPage;
			//	}
			//}

			this.ConvertAllowCommentsForItem(content.mt_allow_comments, newPage);

			// set the author if specified, otherwise use the user that is authenticated by wlw
			// once a post is done, only the 'poster' can modify it
			if (!string.IsNullOrEmpty(content.wp_author_id))
			{
				// get the list of members
				WpAuthor[] authors = this.WpGetAuthors(blog_id, username, password);
				int authorId = Convert.ToInt32(content.wp_author_id);
				string author = authors.First(a => a.user_id == authorId).user_login;
				newPage.Author = author;
			}
			else if (!string.IsNullOrEmpty(content.user_id))
			{
				// get the list of members
				WpAuthor[] authors = this.WpGetAuthors(blog_id, username, password);
				int authorId = Convert.ToInt32(content.user_id);
				string author = authors.First(a => a.user_id == authorId).user_login;
				newPage.Author = author;
			}
			else
			{
				newPage.Author = username;
			}

			this.pageService.SaveOrUpdate(newPage);

			// in the end we need to refresh the routes
			this.routingService.UpdateRoutes();

			return newPage.Id;
		}

		private string ProcessPostData(string blogId, string username, string password, Post post, int? postId)
		{
			string title = HttpUtility.HtmlDecode(post.title);
			string body = post.description;

			PostDto newPost = !postId.HasValue || postId.Value < 1
				                  ? new PostDto()
				                  : this.postService.GetPostByKey(postId.Value);

			newPost.Title = title;
			newPost.Slug = post.wp_slug;
			newPost.Content = body;

			if (!string.IsNullOrEmpty(post.mt_excerpt))
			{
				newPost.Excerpt = post.mt_excerpt;
			}
			else
			{
				newPost.Excerpt = (!string.IsNullOrEmpty(post.description))
					                  ? post.description.CleanHtmlText().Trim().Replace("&nbsp;", string.Empty).Cut(250)
					                  : string.Empty;
			}
			if (newPost.IsTransient)
			{
				newPost.PublishAt = (post.dateCreated == DateTime.MinValue || post.dateCreated == DateTime.MaxValue)
					                    ? DateTimeOffset.Now
					                    : new DateTime(post.dateCreated.Ticks, DateTimeKind.Utc);
			}
			else
			{
				newPost.PublishAt = (post.dateCreated == DateTime.MinValue || post.dateCreated == DateTime.MaxValue)
					                    ? newPost.PublishAt
					                    : new DateTime(post.dateCreated.Ticks, DateTimeKind.Utc);
			}

			BlogConfigurationDto blogConfiguration = this.configurationService.GetConfiguration();

			newPost.Status = newPost.PublishAt.UtcDateTime <= TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), blogConfiguration.TimeZone)
				                 ? ItemStatus.Published
				                 : ItemStatus.Scheduled;

			if (post.categories != null)
			{
				IList<string> categories = post.categories.ToList();
				IList<string> categoriesToAdd = new List<string>();

				IList<CategoryDto> allCategories = this.categoryService.GetCategories();

				foreach (string c in categories)
				{
					CategoryDto cat = allCategories.FirstOrDefault(x => x.Name == c);
					if (cat != null)
					{
						categoriesToAdd.Add(cat.Name);
					}
				}

				newPost.Categories = categoriesToAdd.ToArray();
			}

			IEnumerable<string> tags = this.ExtractTags(post);

			if (tags != null && tags.Any())
			{
				newPost.Tags = tags.ToArray();
			}

			this.ConvertAllowCommentsForItem(post.mt_allow_comments, newPost);

			// set the author if specified, otherwise use the user that is authenticated by wlw
			// once a post is done, only the 'poster' can modify it
			if (!string.IsNullOrEmpty(post.wp_author_id))
			{
				// get the list of members
				WpAuthor[] authors = this.WpGetAuthors(blogId, username, password);
				int authorId = Convert.ToInt32(post.wp_author_id);
				string author = authors.First(a => a.user_id == authorId).user_login;
				newPost.Author = author;
			}
			else if (!string.IsNullOrEmpty(post.userid))
			{
				// get the list of members
				WpAuthor[] authors = this.WpGetAuthors(blogId, username, password);
				int authorId = Convert.ToInt32(post.userid);
				string author = authors.First(a => a.user_id == authorId).user_login;
				newPost.Author = author;
			}
			else
			{
				newPost.Author = username;
			}

			this.postService.SaveOrUpdate(newPost);

			return newPost.Id.ToString(CultureInfo.InvariantCulture);
		}

		private void ValidateUser(string username, string password)
		{
			bool isValid = Membership.ValidateUser(username, password);

			if (!isValid)
			{
				this.logger.Warn("Invalid Credential.");
				throw new XmlRpcFaultException(0, "User is not valid!");
			}

			MetaWeblogIdentity identity = new MetaWeblogIdentity(username, true);

			Thread.CurrentPrincipal = new MetaWeblogPrincipal(identity);
		}

		#endregion
	}

	public class MetaWeblogIdentity : IIdentity
	{
		#region Constructors and Destructors

		public MetaWeblogIdentity(string name, bool isAuthenticated)
		{
			this.Name = name;
			this.AuthenticationType = "XML-RPC";
			this.IsAuthenticated = isAuthenticated;
		}

		#endregion

		#region Public Properties

		public string AuthenticationType { get; private set; }

		public bool IsAuthenticated { get; private set; }

		public string Name { get; private set; }

		#endregion
	}

	public class MetaWeblogPrincipal : IPrincipal
	{
		#region Constructors and Destructors

		public MetaWeblogPrincipal(IIdentity identity)
		{
			this.Identity = identity;
		}

		#endregion

		#region Public Properties

		public IIdentity Identity { get; private set; }

		#endregion

		#region Public Methods and Operators

		public bool IsInRole(string role)
		{
			return Roles.IsUserInRole(role);
		}

		#endregion
	}
}