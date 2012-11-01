#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			DexterOutputCacheProvider.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/11/01
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Caching.Web
{
	using System;
	using System.Web.Caching;

	using Dexter.Dependency;

	public class DexterOutputCacheProvider : OutputCacheProvider
	{
		#region Fields

		private readonly ICacheProvider provider;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Web.Caching.OutputCacheProvider" /> class.
		/// </summary>
		public DexterOutputCacheProvider(ICacheProvider provider)
		{
			this.provider = provider;
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref="T:System.Web.Caching.OutputCacheProvider" /> class.
		/// </summary>
		public DexterOutputCacheProvider()
		{
			this.provider = DexterContainer.Resolve<ICacheProvider>();
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// 	Inserts the specified entry into the output cache.
		/// </summary>
		/// <returns> A reference to the specified provider. </returns>
		/// <param name="key"> A unique identifier for <paramref name="entry" /> . </param>
		/// <param name="entry"> The content to add to the output cache. </param>
		/// <param name="utcExpiry"> The time and date on which the cached entry expires. </param>
		public override object Add(string key, object entry, DateTime utcExpiry)
		{
			this.provider.Add(key, entry, utcExpiry.Subtract(DateTime.Now));

			return entry;
		}

		/// <summary>
		/// 	Returns a reference to the specified entry in the output cache.
		/// </summary>
		/// <returns> The <paramref name="key" /> value that identifies the specified entry in the cache, or null if the specified entry is not in the cache. </returns>
		/// <param name="key"> A unique identifier for a cached entry in the output cache. </param>
		public override object Get(string key)
		{
			return this.provider.Get(key);
		}

		/// <summary>
		/// 	Removes the specified entry from the output cache.
		/// </summary>
		/// <param name="key"> The unique identifier for the entry to remove from the output cache. </param>
		public override void Remove(string key)
		{
			this.provider.Remove(key);
		}

		/// <summary>
		/// 	Inserts the specified entry into the output cache, overwriting the entry if it is already cached.
		/// </summary>
		/// <param name="key"> A unique identifier for <paramref name="entry" /> . </param>
		/// <param name="entry"> The content to add to the output cache. </param>
		/// <param name="utcExpiry"> The time and date on which the cached <paramref name="entry" /> expires. </param>
		public override void Set(string key, object entry, DateTime utcExpiry)
		{
			this.provider.Put(key, entry, utcExpiry.Subtract(DateTime.Now));
		}

		#endregion
	}
}