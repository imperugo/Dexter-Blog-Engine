#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			ItemComments.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/28
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
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

	using Dexter.Entities;

	using global::Raven.Imports.Newtonsoft.Json;

	public class ItemComments : EntityBase<string>
	{
		#region Constructors and Destructors

		public ItemComments()
		{
			this.Approved = new List<Comment>();
			this.Pending = new List<Comment>();
			this.Spam = new List<Comment>();
		}

		#endregion

		#region Public Properties

		public List<Comment> Approved { get; set; }

		public ItemReference Item { get; set; }

		public List<Comment> Pending { get; set; }

		public List<Comment> Spam { get; set; }

		[JsonIgnore]
		public int TotalApprovedComments
		{
			get
			{
				return this.Approved.Count;
			}
		}

		[JsonIgnore]
		public int TotalPendingComments
		{
			get
			{
				return this.Pending.Count;
			}
		}

		[JsonIgnore]
		public int TotalSpamComments
		{
			get
			{
				return this.Spam.Count;
			}
		}

		#endregion

		#region Public Methods and Operators

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
						this.Pending.Remove(itemToRemove);
						break;
					}

				case CommentStatus.IsSpam:
					{
						itemToRemove = this.Spam.SingleOrDefault(x => x.Id == commentId);
						this.Spam.Remove(itemToRemove);
						break;
					}

				case CommentStatus.IsApproved:
					{
						itemToRemove = this.Approved.SingleOrDefault(x => x.Id == commentId);
						this.Approved.Remove(itemToRemove);
						break;
					}

				default:
					{
						throw new ArgumentException("Unable to find a comment with the specified id", "commentId");
					}
			}
		}

		#endregion
	}
}