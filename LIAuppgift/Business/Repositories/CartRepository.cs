namespace LIAuppgift.Business.Repositories
{
    using LIAuppgift.Business.EntityFramework;
    using LIAuppgift.Models.Entites;
    using System.Collections.Generic;
    using System.Linq;

    // A repository "talks to the db" CRUD
    public class CartRepository
    {
        /// Adds an entity
        public void Add(CartItemEntity item)
        {
            using (CartContext ctx = new CartContext())
            {
                ctx.CartItems.Add(item);
                ctx.SaveChanges();
            }
        }

        // Gets a single identity
        public CartItemEntity Get(int id)
        {
            using (CartContext ctx = new CartContext())
            {
                var cartItems = ctx.CartItems.SingleOrDefault(x => x.Id == id);
                return cartItems;
            }
        }

        // Gets entities with a key (id) and lists it's properties
        public IEnumerable<CartItemEntity> Get(string userId)
        {
            using (CartContext ctx = new CartContext())
            {
                var cartItems = ctx.CartItems
                    .Where(x => x.UserId == userId).ToList()
                    .GroupBy(item => item.ProductId)
                    .Select(group => new CartItemEntity
                    {
                        ProductId = group.Key,
                        ProductName = group.First().ProductName,
                        Quantity = group.Count(),
                        ConvertedPrice = group.First().ConvertedPrice,
                        SumPrice = group.First().ConvertedPrice * group.Count()
                    }).ToList();

                return cartItems;
            }
        }
    }
}