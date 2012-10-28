#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ItemComments.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Domain
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ItemComments
	{
		private ItemComments()
		{
		}

		public ItemComments(int itemId)
		{
			this.ItemId = itemId;
			this.Approved = new List<Comment>();
			this.Deleted = new List<Comment>();
			this.Pending = new List<Comment>();
			this.Spam = new List<Comment>();
		}

		#region Public Properties

		public List<Comment> Approved { get; set; }

		public List<Comment> Deleted { get; set; }

		public int ItemId { get; set; }

		public List<Comment> Pending { get; set; }

		public List<Comment> Spam { get; set; }

		#endregion

		public void AddComment(Comment item, CommentStatus status)
		{
			switch (status)
			{
				case CommentStatus.Pending:
					{
						this.Pending.Add(item);
						break;
					}
				case CommentStatus.IsSpam:
					{
						this.Spam.Add(item);
						break;
					}
				case CommentStatus.IsApproved:
					{
						this.Approved.Add(item);
						break;
					}
				default:
					{
						throw new ArgumentException("Unable to add a comment for the specified status", "status");
					}
			}
		}

		public void Delete(int commentId, CommentStatus status)
		{
			Comment itemToRemove;

			switch (status)
			{
					case CommentStatus.Pending:
					{
						itemToRemove = this.Pending.SingleOrDefault(x => x.Id == commentId);
						this.Deleted.Add(itemToRemove);
						this.Pending.Remove(itemToRemove);
						break;
					}
					case CommentStatus.IsSpam:
					{
						itemToRemove = this.Spam.SingleOrDefault(x => x.Id == commentId);
						this.Deleted.Add(itemToRemove);
						this.Spam.Remove(itemToRemove);
						break;
					}
					case CommentStatus.IsApproved:
					{
						itemToRemove = this.Approved.SingleOrDefault(x => x.Id == commentId);
						this.Deleted.Add(itemToRemove);
						this.Approved.Remove(itemToRemove);
						break;
					}
					default:
					{
						throw new ArgumentException("Unable to find a comment with the specified id", "commentId");
					}
			}
		}
	}
}