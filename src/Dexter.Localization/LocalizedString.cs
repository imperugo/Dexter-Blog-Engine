#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			LocalizedString.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/DexterBlogEngine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Localization
{
	using System.Web;

	public class LocalizedString : IHtmlString
	{
		#region Fields

		private readonly object[] args;

		private readonly string localized;

		private readonly string msgId;

		#endregion

		#region Constructors and Destructors

		public LocalizedString(string languageNeutral)
		{
			this.localized = languageNeutral;
			this.msgId = languageNeutral;
		}

		public LocalizedString(string localized, string msgId, object[] args)
		{
			this.localized = localized;
			this.msgId = msgId;
			this.args = args;
		}

		#endregion

		#region Public Properties

		public object[] Args
		{
			get
			{
				return this.args;
			}
		}

		public string MsgId
		{
			get
			{
				return this.msgId;
			}
		}

		public string Text
		{
			get
			{
				return this.localized;
			}
		}

		#endregion

		#region Public Methods and Operators

		public static LocalizedString TextOrDefault(string text, LocalizedString defaultValue)
		{
			if (string.IsNullOrEmpty(text))
			{
				return defaultValue;
			}

			return new LocalizedString(text);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != this.GetType())
			{
				return false;
			}

			LocalizedString that = (LocalizedString)obj;
			return string.Equals(this.localized, that.localized);
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			if (this.localized != null)
			{
				hashCode ^= this.localized.GetHashCode();
			}

			return hashCode;
		}

		public override string ToString()
		{
			return string.Format(this.localized, this.args);
		}

		#endregion

		#region Explicit Interface Methods

		string IHtmlString.ToHtmlString()
		{
			return string.Format(this.localized, this.args);
		}

		#endregion
	}
}