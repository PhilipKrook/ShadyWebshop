namespace LIAuppgift.Models.Migrations
{   
    using System.Data.Entity.Migrations;   

    internal sealed class EPiServerDbContextConfiguration : DbMigrationsConfiguration<Business.EntityFramework.EPiServerDbContext>
    {
        public EPiServerDbContextConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.MigrationsDirectory = @"Migrations\Identity";
            this.ContextKey = "EPiServer.Cms.UI.AspNetIdentity.ApplicationDbContext`1[Models.Entities.CustomUser]";
        }

        protected override void Seed(Business.EntityFramework.EPiServerDbContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}