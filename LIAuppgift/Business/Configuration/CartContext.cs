namespace LIAuppgift.Controllers
{
    using System.Data.Entity;

    public class CartContext : DbContext
    {
        public CartContext() : base("name=EPiServerDB")
        {
        }

        public DbSet<CartItemEntity> CartItems { get; set; }
    }
}