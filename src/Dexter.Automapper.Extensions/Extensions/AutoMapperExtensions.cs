#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			AutoMapperExtensions.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/23
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace AutoMapper
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public static class AutoMapperExtensions
	{
		#region Public Methods and Operators

		public static TResult DynamicMapTo<TResult>(this object self)
		{
			if (self == null)
			{
				throw new ArgumentNullException();
			}

			return (TResult)Mapper.DynamicMap(self, self.GetType(), typeof(TResult));
		}

		public static TResult MapPropertiesToInstance<TResult>(this object self, TResult value)
		{
			if (self == null)
			{
				throw new ArgumentNullException();
			}

			return (TResult)Mapper.Map(self, value, self.GetType(), typeof(TResult));
		}

		public static List<TResult> MapTo<TResult>(this IEnumerable self)
		{
			if (self == null)
			{
				throw new ArgumentNullException();
			}

			return (List<TResult>)Mapper.Map(self, self.GetType(), typeof(List<TResult>));
		}

		public static TResult MapTo<TResult>(this object self)
		{
			if (self == null)
			{
				throw new ArgumentNullException();
			}

			return (TResult)Mapper.Map(self, self.GetType(), typeof(TResult));
		}

		#endregion
	}
}