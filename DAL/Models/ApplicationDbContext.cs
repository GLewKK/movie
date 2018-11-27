using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		#region Dbsets

		//Dbsets

		#endregion

		public static ApplicationDbContext Create()
		{

			return new ApplicationDbContext();
		}
	}
}