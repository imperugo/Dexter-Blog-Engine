#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PublishingJob.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/24
// Last edit:	2013/05/09
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Scheduler.Jobs
{
	using System;

	using AutoMapper;

	using Dexter.Shared.Dto;
	using Dexter.Services;
	using Dexter.Shared.Filters;
	using Dexter.Shared.Requests;
	using Dexter.Shared.Result;

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
			IPagedResult<PostDto> posts = this.postService.GetPosts(1, 50, new ItemQueryFilter
				                                                               {
					                                                               Status = ItemStatus.Scheduled, 
					                                                               MinPublishAt = DateTimeOffset.Now.AddMinutes(-5).AsMinutes(), 
					                                                               MaxPublishAt = DateTimeOffset.Now
				                                                               });

			foreach (PostDto post in posts.Result)
			{
				post.Status = ItemStatus.Published;
				this.postService.SaveOrUpdate(post.MapTo<PostRequest>());
			}
		}
	}
}