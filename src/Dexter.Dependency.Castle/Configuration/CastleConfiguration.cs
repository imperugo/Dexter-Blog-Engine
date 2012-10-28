#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CastleConfiguration.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Dependency.Castle.Configuration
{
	using global::Castle.DynamicProxy;

	using global::Castle.MicroKernel.Registration;

	using global::Castle.Windsor;

	/// <summary>
	/// 	This class contains the Windsor configuration
	/// </summary>
	internal static class CastleConfiguration
	{
		#region Public Methods and Operators

		public static void RegisterInterceptor(IWindsorContainer container)
		{
			container.Register(AllTypes.FromAssembly(typeof(CastleConfiguration).Assembly).BasedOn<IInterceptor>().Configure(reg => reg.LifeStyle.Singleton.Named(reg.Implementation.Name)).WithService.FromInterface());
		}

		#endregion
	}
}