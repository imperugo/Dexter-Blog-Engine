#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ITrackbackService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/11/12
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data
{
	using System.Threading.Tasks;

	using Dexter.Entities;

	public interface ITrackbackService
	{
		#region Public Methods and Operators

		Task SaveOrUpdateAsync(TrackBackDto trackBack, ItemType itemType);

		#endregion
	}
}