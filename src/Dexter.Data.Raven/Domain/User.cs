#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			User.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/12/23
// Last edit:	2012/12/23
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

	public class User : EntityBase<string>
	{
		#region Fields

		private IList<string> roles;

		#endregion

		#region Constructors and Destructors

		public User()
		{
			this.Roles = new List<string>();
		}

		#endregion

		#region Public Properties

		public string ApplicationName { get; set; }

		public string Comment { get; set; }

		public string ConfirmationToken { get; set; }

		public DateTime? CreateDate { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public bool IsApproved { get; set; }

		public bool IsLockedOut { get; set; }

		public DateTime? LastActivityDate { get; set; }

		public DateTime? LastLockoutDate { get; set; }

		public DateTime? LastLoginDate { get; set; }

		public string LastName { get; set; }

		public DateTime? LastPasswordChangedDate { get; set; }

		public DateTime? LastPasswordFailureDate { get; set; }

		public string Password { get; set; }

		public int PasswordFailuresSinceLastSuccess { get; set; }

		public string PasswordVerificationToken { get; set; }

		public DateTime? PasswordVerificationTokenExpirationDate { get; set; }

		public IEnumerable<string> Roles
		{
			get
			{
				return this.roles;
			}
			set
			{
				this.roles = (IList<string>)value ?? new List<string>(0);
			}
		}

		public Guid UserId { get; set; }

		public string Username { get; set; }

		#endregion

		#region Public Methods and Operators

		public bool AddRole(string rolename)
		{
			if (!this.roles.Contains(rolename))
			{
				this.roles.Add(rolename);
				return true;
			}
			return false;
		}

		public IEnumerable<string> GetRolesToRemove(IEnumerable<string> newRoles)
		{
			return this.Roles
			           .Except(newRoles)
			           .ToList();
		}

		public bool RemoveRole(string roleName)
		{
			return this.roles.Remove(roleName);
		}

		public void UpdateUser(User user)
		{
			this.Username = user.Username;
			this.Email = user.Email;
			this.Password = user.Password;
			this.ApplicationName = user.ApplicationName;
			this.Comment = user.Comment;
			this.ConfirmationToken = user.ConfirmationToken;
			this.CreateDate = user.CreateDate;
			this.IsApproved = user.IsApproved;
			this.IsLockedOut = user.IsLockedOut;
			this.LastActivityDate = user.LastActivityDate;
			this.LastLockoutDate = user.LastLockoutDate;
			this.LastLoginDate = user.LastLoginDate;
			this.LastPasswordChangedDate = user.LastPasswordChangedDate;
			this.LastPasswordFailureDate = user.LastPasswordFailureDate;
			this.PasswordFailuresSinceLastSuccess = user.PasswordFailuresSinceLastSuccess;
			this.PasswordVerificationToken = user.PasswordVerificationToken;
			this.PasswordVerificationTokenExpirationDate = user.PasswordVerificationTokenExpirationDate;
		}

		#endregion
	}
}