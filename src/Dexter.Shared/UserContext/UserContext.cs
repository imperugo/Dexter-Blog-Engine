namespace Dexter.Shared.UserContext
{
	using System.Threading;

	public class UserContext : IUserContext
	{
		#region Public Properties

		public bool IsAuthenticated
		{
			get
			{
				return Thread.CurrentPrincipal.Identity.IsAuthenticated;
			}
		}

		public string Username
		{
			get
			{
				return Thread.CurrentPrincipal.Identity.Name;
			}
		}

		#endregion

		#region Public Methods and Operators

		public bool IsInRole(string roleName)
		{
			return Thread.CurrentPrincipal.IsInRole(roleName);
		}

		#endregion
	}
}