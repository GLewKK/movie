using static System.Guid;

namespace DAL.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAL.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            if (!context.Image.Any())
            {
                context.Image.AddOrUpdate(x => x.Id,
                    new Image() {Id = NewGuid(), FileName = "default-user.png", FilePath = "~/DAL/Assets/"}
                );
            }

            if (!context.Statuses.Any())
            {

            context.Statuses.AddOrUpdate(x => x.Id,
                new Status() { Id=NewGuid(), Name="pending for validation"},
                new Status() { Id = NewGuid(), Name = "pending for approval" },
                new Status() { Id = NewGuid(), Name = "approved" },
                new Status() { Id = NewGuid(), Name = "denied" },
                new Status() { Id = NewGuid(), Name = "rejected" },
                new Status() { Id = NewGuid(), Name = "not-approved" },
                new Status() { Id = NewGuid(), Name = "cancelled" }
            );
            }

            if (!context.Telephones.Any())
            {

            context.Telephones.AddOrUpdate(x=>x.Id,
                new Telephone() { Id=NewGuid(), CountryName="Moldova", NumberCode="+373", NumberExample="+37360973939"}

                );
            }

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
