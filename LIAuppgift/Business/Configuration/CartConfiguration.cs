namespace LIAuppgift.Business.Configuration
{
    using LIAuppgift.Business.EntityFramework;
    using System.Data.Entity.Migrations;

    internal sealed class CartConfiguration : DbMigrationsConfiguration<CartContext>
    {
        public CartConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }
}