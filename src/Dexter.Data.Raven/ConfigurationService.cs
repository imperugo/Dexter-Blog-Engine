#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ConfigurationService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven
{
	using Common.Logging;

	using global::Raven.Client;

	public class ConfigurationService : ServiceBase
	{
		#region Constructors and Destructors

		public ConfigurationService(ILog logger, IDocumentSession session)
			: base(logger, session)
		{
		}

		#endregion

		#region Public Methods and Operators

		public void GetConfiguration()
		{
		}

		#endregion
	}
}