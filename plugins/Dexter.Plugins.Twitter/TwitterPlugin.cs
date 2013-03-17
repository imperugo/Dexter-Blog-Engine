#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TwitterPlugin.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/11
// Last edit:	2013/03/13
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Plugins.Twitter
{
	using System;

	using Dexter.Entities;
	using Dexter.Services;
	using Dexter.Services.Events;
	using Dexter.Services.Plugins;

	public class TwitterPlugin : IPlugin
	{
		#region Constructors and Destructors

		public TwitterPlugin(IPostService postService)
		{
			postService.PostSaved += this.PostSaved;
		}

		#endregion

		#region Public Methods and Operators

		public void Initialize()
		{
			
		}

		public void Setup()
		{
		}

		#endregion

		#region Methods

		private void PostSaved(object sender, CancelEventArgsWithoutParameterWithResult<PostDto> e)
		{
			if (e.Result.PublishAt >= DateTimeOffset.Now)
			{
				return;
			}
		}

		#endregion
	}
}