namespace LIAuppgift.Business.Repositories
{
    using LIAuppgift.Business.EntityFramework;
    using LIAuppgift.Models.Entites;
    using System.Collections.Generic;
    using System.Linq;

    public class CartRepository
    {
        public void Add(CartItemEntity item)
        {
            using (CartContext ctx = new CartContext())
            {
                ctx.CartItems.Add(item);
                ctx.SaveChanges();
            }
        }

        public CartItemEntity Get(int id)
        {
            using (CartContext ctx = new CartContext())
            {
                var cartItems = ctx.CartItems.SingleOrDefault(x => x.Id == id);
                return cartItems;
            }
        }

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
                        Price = group.First().Price,
                        SumPrice = group.First().Price * group.Count()
                    }).ToList();

                return cartItems;
            }
        }
    }
}