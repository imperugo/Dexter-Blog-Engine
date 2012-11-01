#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ICacheProvider.cs
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

namespace Dexter.Caching
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// 	The base contract for cache provider implementation.
	/// </summary>
	public interface ICacheProvider
	{
		#region Public Methods and Operators

		///<summary>
		///	Adds new object into the cache repository
		///</summary>
		///<param name="key"> The cache key. </param>
		///<param name="value"> The object to add. </param>
		///<param name="timeout"> The timeout of the cache object. </param>
		///<remarks>
		///	If the object will never expire, use <see cref="TimeSpan.Zero" />
		///</remarks>
		///<exception cref="ArgumentException">Will be throw if
		///	<paramref name="key" />
		///	is empty.</exception>
		///<exception cref="ArgumentNullException">Will be throw if
		///	<paramref name="key" />
		///	is null.</exception>
		void Add(string key, object value, TimeSpan timeout);

		///<summary>
		///	Adds new object into the cache repository
		///</summary>
		///<param name="key"> The cache key. </param>
		///<param name="value"> The object to add. </param>
		///<param name="timeout"> The timeout of the cache object. </param>
		///<remarks>
		///	If the object will never expire, use <see cref="TimeSpan.Zero" />
		///</remarks>
		///<exception cref="ArgumentException">Will be throw if
		///	<paramref name="key" />
		///	is empty.</exception>
		///<exception cref="ArgumentNullException">Will be throw if
		///	<paramref name="key" />
		///	is null.</exception>
		Task AddAsync(string key, object value, TimeSpan timeout);

		/// <summary>
		/// 	Return a set of cache objects with only one round-trip
		/// </summary>
		/// <param name="keys"> All the cache key. </param>
		/// <returns> A collection of cache objects. </returns>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="keys" />
		/// 	is null.</exception>
		IEnumerable<KeyValuePair<string, object>> BulkGet(IEnumerable<string> keys);

		/// <summary>
		/// 	Return a set of cache objects with only one round-trip
		/// </summary>
		/// <param name="keys"> All the cache key. </param>
		/// <returns> A collection of cache objects. </returns>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="keys" />
		/// 	is null.</exception>
		Task<IEnumerable<KeyValuePair<string, object>>> BulkGetAsync(IEnumerable<string> keys);

		/// <summary>
		/// 	Remove all items into the cache repository
		/// </summary>
		void Clear();

		/// <summary>
		/// 	Remove all items into the cache repository
		/// </summary>
		Task ClearAsync();

		/// <summary>
		/// 	Retrieve the cache object for the specified cache key.
		/// </summary>
		/// <param name="key"> The key. </param>
		/// <returns> The cache object </returns>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		object Get(string key);

		/// <summary>
		/// 	Retrieve the cache object for the specified cache key.
		/// </summary>
		/// <param name="key"> The key. </param>
		/// <returns> The cache object </returns>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		T Get<T>(string key);

		/// <summary>
		/// 	Retrieve the cache object for the specified cache key.
		/// </summary>
		/// <param name="key"> The key. </param>
		/// <returns> The cache object </returns>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		Task<object> GetAsync(string key);

		/// <summary>
		/// 	Retrieve the cache object for the specified cache key.
		/// </summary>
		/// <param name="key"> The key. </param>
		/// <returns> The cache object </returns>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		Task<T> GetAsync<T>(string key);

		///<summary>
		///	If the specified object doesn't exist into the repository, it will be added. Otherwise it will update the existing object.
		///</summary>
		///<param name="key"> The cache key. </param>
		///<param name="value"> The object to add. </param>
		///<param name="timeout"> The timeout of the cache object. </param>
		///<remarks>
		///	If the object will never expire, use <see cref="TimeSpan.Zero" />
		///</remarks>
		///<exception cref="ArgumentException">Will be throw if
		///	<paramref name="key" />
		///	is empty.</exception>
		///<exception cref="ArgumentNullException">Will be throw if
		///	<paramref name="key" />
		///	is null.</exception>
		void Put(string key, object value, TimeSpan timeout);

		///<summary>
		///	If the specified object doesn't exist into the repository, it will be added. Otherwise it will update the existing object.
		///</summary>
		///<param name="key"> The cache key. </param>
		///<param name="value"> The object to add. </param>
		///<param name="timeout"> The timeout of the cache object. </param>
		///<remarks>
		///	If the object will never expire, use <see cref="TimeSpan.Zero" />
		///</remarks>
		///<exception cref="ArgumentException">Will be throw if
		///	<paramref name="key" />
		///	is empty.</exception>
		///<exception cref="ArgumentNullException">Will be throw if
		///	<paramref name="key" />
		///	is null.</exception>
		Task PutAsync(string key, object value, TimeSpan timeout);

		/// <summary>
		/// 	Remove a cache object with the specified key from the cache repository.
		/// </summary>
		/// <param name="key"> The key. </param>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		void Remove(string key);

		/// <summary>
		/// 	Remove a cache object with the specified key from the cache repository.
		/// </summary>
		/// <param name="key"> The key. </param>
		/// <exception cref="ArgumentException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is empty.</exception>
		/// <exception cref="ArgumentNullException">Will be throw if
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		Task RemoveAsync(string key);

		#endregion
	}
}