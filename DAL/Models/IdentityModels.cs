﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<DataExtra> DataExtra { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Telephone> Telephones { get; set; }
        public DbSet<FormErrors> FormErrorses { get; set; }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }
    }
}