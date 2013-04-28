#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			XmlExtensions.cs
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

namespace System.Xml
{
	using System.Xml.Linq;

	public static class XmlExtensions
	{
		#region Public Methods and Operators

		public static XDocument ToXDocument(this XmlDocument xmlDocument)
		{
			using (XmlNodeReader nodeReader = new XmlNodeReader(xmlDocument))
			{
				nodeReader.MoveToContent();
				return XDocument.Load(nodeReader);
			}
		}

		public static XmlDocument ToXmlDocument(this XDocument xDocument)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (XmlReader xmlReader = xDocument.CreateReader())
			{
				xmlDocument.Load(xmlReader);
			}
			return xmlDocument;
		}

		#endregion
	}
}