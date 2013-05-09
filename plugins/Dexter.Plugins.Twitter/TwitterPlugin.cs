#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TwitterPlugin.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/11
// Last edit:	2013/04/01
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Plugins.Twitter
{
	using Dexter.Data;
	using Dexter.Shared.Dto;
	using Dexter.Services;
	using Dexter.Services.Events;
	using Dexter.Services.Plugins;

	public class TwitterPlugin : IPlugin
	{
		#region Fields

		private readonly IPluginDataService pluginDataService;

		#endregion

		#region Constructors and Destructors

		public TwitterPlugin(IPostService postService, IPluginDataService pluginDataService)
		{
			this.pluginDataService = pluginDataService;
			postService.PostPublished += this.PostPublished;
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

		private void PostPublished(object sender, CancelEventArgsWithoutParameterWithResult<PostDto> e)
		{
		}

		#endregion
	}
}