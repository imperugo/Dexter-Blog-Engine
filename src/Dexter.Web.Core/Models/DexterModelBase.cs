#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterModelBase.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Web.Core.Models
{
	using System;
	using System.Collections.Generic;
	using System.Dynamic;

	using Dexter.Entities;

	public class DexterModelBase : DynamicObject
	{
		#region Fields

		private readonly IDictionary<String, object> customProperties = new Dictionary<String, object>();

		private readonly object lockObject = new object();

		private string copyRight;

		private string description;

		private string keywords;

		private string title;

		#endregion

		#region Public Properties

		public virtual string Author { get; set; }

		public virtual BlogConfiguration Configuration { get; internal set; }

		public virtual string CopyRight
		{
			get
			{
				if (string.IsNullOrEmpty(this.copyRight) && this.Configuration != null)
				{
					return this.Configuration.SeoConfiguration.CopyRight;
				}

				return this.copyRight;
			}
			set
			{
				this.copyRight = value;
			}
		}

		public virtual string Description
		{
			get
			{
				if (string.IsNullOrEmpty(this.description) && this.Configuration != null)
				{
					return this.Configuration.SeoConfiguration.DefaultDescription;
				}

				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		public virtual string KeyWords
		{
			get
			{
				if (string.IsNullOrEmpty(this.keywords) && this.Configuration != null)
				{
					return string.Join(",", this.Configuration.SeoConfiguration.DefaultKeyWords);
				}

				return this.keywords;
			}
			set
			{
				this.keywords = value;
			}
		}

		public virtual IEnumerable<CommentDto> RecentComments { get; internal set; }

		public virtual IEnumerable<PostDto> RecentPosts { get; internal set; }

		public virtual string Title
		{
			get
			{
				if (string.IsNullOrEmpty(this.title) && this.Configuration != null)
				{
					return this.Configuration.SeoConfiguration.DefaultTitle;
				}

				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Provides the implementation for operations that get member values. Classes derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
		/// </summary>
		/// <param name = "binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name = "result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name = "result" />.</param>
		/// <returns>
		/// 	true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
		/// </returns>
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (this.customProperties.ContainsKey(binder.Name))
			{
				result = this.customProperties[binder.Name];
			}
			else
			{
				result = binder.ReturnType.IsValueType
					         ? Activator.CreateInstance(binder.ReturnType)
					         : null;
				this.customProperties.Add(binder.Name, result);
			}
			return true;
		}

		/// <summary>
		/// 	Provides the implementation for operations that set member values. Classes derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
		/// </summary>
		/// <param name = "binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name = "value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref = "T:System.Dynamic.DynamicObject" /> class, the <paramref name = "value" /> is "Test".</param>
		/// <returns>
		/// 	true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
		/// </returns>
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			lock (this.lockObject)
			{
				this.customProperties[binder.Name] = value;
			}

			return true;
		}

		#endregion
	}
}