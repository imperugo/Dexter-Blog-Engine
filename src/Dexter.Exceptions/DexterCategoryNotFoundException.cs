#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterCategoryNotFoundException.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/24
// Last edit:	2013/03/24
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Exceptions
{
	public class DexterCategoryNotFoundException : DexterException
	{
		#region Fields

		private readonly int categoryId;

		private readonly string categoryName;

		#endregion

		#region Constructors and Destructors

		public DexterCategoryNotFoundException(string message, string categoryName)
			: base(message)
		{
			this.categoryName = categoryName;
		}

		public DexterCategoryNotFoundException(int categoryId)
		{
			this.categoryId = categoryId;
		}

		public DexterCategoryNotFoundException(string message, int categoryId)
			: base(message)
		{
			this.categoryId = categoryId;
		}

		public DexterCategoryNotFoundException(string categoryName)
		{
			this.categoryName = categoryName;
		}

		public DexterCategoryNotFoundException()
		{
		}

		#endregion

		#region Public Properties

		public int CategoryId
		{
			get
			{
				return this.categoryId;
			}
		}

		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		#endregion
	}
}