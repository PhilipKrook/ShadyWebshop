namespace LIAuppgift.Controllers
{
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
                        Name = group.First().Name,
                        Quantity = group.Count(),
                        Price = group.First().Price,
                        SumPrice = group.First().Price * group.Count()
                    }).ToList();

                return cartItems;
            }
        }
    }
}