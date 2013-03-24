using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dexter.Scheduler.Jobs
{
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Services;

	using global::Quartz;

	public class PublishingJob : IJob
	{
		private readonly IPostService postService;

		public PublishingJob(IPostService postService)
		{
			this.postService = postService;
		}

		public void Execute(IJobExecutionContext context)
		{
			IPagedResult<PostDto> posts = postService.GetPosts(1, 50, new ItemQueryFilter
				                                                          {
					                                                          Status = ItemStatus.Scheduled,
					                                                          MinPublishAt = DateTimeOffset.Now.AddMinutes(-5).AsMinutes(),
					                                                          MaxPublishAt = DateTimeOffset.Now
				                                                          });

			foreach (var post in posts.Result)
			{
				post.Status = ItemStatus.Published;
				postService.SaveOrUpdate(post);
			}
		}
	}
}
