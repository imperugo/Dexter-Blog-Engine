#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Helper.cs
// Website:		http://www.easydom.it/
// Created:		2013/03/31
// Last edit:	2013/03/31
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Gaia.Localization.Po.Tests
{
	using System;
	using System.IO;

	internal static class Helper
	{
		#region Public Methods and Operators

		public static Stream GetTestModuleStream()
		{
			string path = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\App_Data\\Localization\\en-US\\TestModule.po";
			return new FileStream(path, FileMode.Open, FileAccess.Read);
		}

		#endregion
	}
}