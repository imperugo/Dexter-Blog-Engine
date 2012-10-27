namespace Dexter.Caching.InProc
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Caching;

	public class InProcCacheProvider : ICacheProvider
	{
		#region Fields

		private readonly HttpContextWrapper cacheRepository;

		#endregion

		#region Constructors and Destructors

		public InProcCacheProvider()
			: this(new HttpContextWrapper(HttpContext.Current))
		{
		}

		public InProcCacheProvider(HttpContextWrapper cacheRepository)
		{
			this.cacheRepository = cacheRepository;
		}

		#endregion

		#region Public Methods and Operators

		public void Add(string key, object value, TimeSpan timeout)
		{
			this.ValidateParameter(key, "key");
			this.ValidateParameter(value, "value");

			this.cacheRepository.Cache.Add(key, value, null, DateTime.Now.Add(timeout), TimeSpan.Zero, CacheItemPriority.Normal, null);
		}

		public Task AddAsync(string key, object value, TimeSpan timeout)
		{
			return Task.Run(() => this.Add(key, value, timeout));
		}

		public IEnumerable<KeyValuePair<string, object>> BulkGet(IEnumerable<string> keys)
		{
			this.ValidateParameter(keys, "keys");

			return keys.Select(key => new KeyValuePair<string, object>(key, this.Get(key))).ToList();
		}

		public Task<IEnumerable<KeyValuePair<string, object>>> BulkGetAsync(IEnumerable<string> keys)
		{
			return Task.Run(() => this.BulkGet(keys));
		}

		public void Clear()
		{
			foreach (DictionaryEntry cache in this.cacheRepository.Cache)
			{
				this.cacheRepository.Cache.Remove((string)cache.Key);
			}
		}

		public Task ClearAsync()
		{
			return Task.Run(() => this.Clear());
		}

		public object Get(string key)
		{
			this.ValidateParameter(key, "key");

			return this.cacheRepository.Cache.Get(key);
		}

		public T Get<T>(string key)
		{
			this.ValidateParameter(key, "key");

			object cacheObject = this.cacheRepository.Cache.Get(key);

			if (cacheObject == null)
			{
				return default(T);
			}

			return (T)cacheObject;
		}

		public Task<object> GetAsync(string key)
		{
			return Task.Run(() => this.Get(key));
		}

		public Task<T> GetAsync<T>(string key)
		{
			return Task.Run(() => this.GetAsync<T>(key));
		}

		public void Put(string key, object value, TimeSpan timeout)
		{
			this.ValidateParameter(key, "key");
			this.ValidateParameter(value, "value");

			this.cacheRepository.Cache.Insert(key, value, null, DateTime.Now.Add(timeout), TimeSpan.Zero, CacheItemPriority.Normal, null);
		}

		public Task PutAsync(string key, object value, TimeSpan timeout)
		{
			return Task.Run(() => this.PutAsync(key, value, timeout));
		}

		public void Remove(string key)
		{
			this.ValidateParameter(key, "key");
			this.cacheRepository.Cache.Remove(key);
		}

		public Task RemoveAsync(string key)
		{
			return Task.Run(() => this.Remove(key));
		}

		#endregion

		#region Methods

		private void ValidateParameter(object obj, string parameterName)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(parameterName);
			}

			if (obj is string && (string)obj == string.Empty)
			{
				throw new ArgumentException("The string must have a value.", parameterName);
			}
		}

		#endregion
	}
}