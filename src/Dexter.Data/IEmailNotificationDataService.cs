#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IEmailNotificationDataService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/12/31
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data
{
	using System.Collections.Generic;

	using Dexter.Shared.Dto;

	public interface IEmailNotificationDataService
	{
		#region Public Methods and Operators

		/// <summary>
		/// 	The lifecycle of the item is completed, so this method removes it from the repository.
		/// </summary>
		/// <param name = "item">The item to remove.</param>
		void Dequeue(EmailMessageDto item);

		/// <summary>
		/// 	Peeks a set of <see cref="EmailMessageDto"/> from the queue.
		/// </summary>
		/// <param name = "size">The size of return's array.</param>
		/// <returns>
		/// 	An array of <c>EmailMessage</c>.
		/// 	The return object could be null if there isn't items into it.
		/// </returns>
		IEnumerable<EmailMessageDto> Peek(int size);

		/// <summary>
		/// 	Queues the specified email into dispatcher list.
		/// </summary>
		/// <param name = "item">The item.</param>
		void Queue(EmailMessageDto item);

		#endregion
	}
}