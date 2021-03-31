namespace LIAuppgift.Business.Repositories
{
    using LIAuppgift.Business.EntityFramework;
    using LIAuppgift.Models.Entites;
    using System.Collections.Generic;
    using System.Linq;

    // A repository "talks to the db" CRUD
    public class CartRepository
    {
        // Adds an entity
        public void Save(CartItemEntity itemValidate, bool addOne)
        {
            using (CartContext ctx = new CartContext())
            {
                var cartItem = ctx.CartItems.SingleOrDefault(x => x.ProductId == itemValidate.ProductId && x.UserId == itemValidate.UserId);
                if (cartItem == null)
                {
                    ctx.CartItems.Add(itemValidate);
                }
                else
                {
                    cartItem.Quantity = addOne ? cartItem.Quantity+1: itemValidate.Quantity;
                }
                ctx.SaveChanges();
            }
        }

        // Removes one product type from the cart
        public void Remove(int productId, string userId)
        {
            using (CartContext ctx = new CartContext())
            {
                var cartItems = ctx.CartItems.Where(x => x.ProductId == productId && x.UserId == userId);
                ctx.CartItems.RemoveRange(cartItems);
                ctx.SaveChanges();
            }
        }

        // Changes the quantity of a product in the cart
        public void Update(int productId, int quantity, string userId)
        {
            using (var ctx = new CartContext())
            {
                var item = ctx.CartItems.SingleOrDefault(i => i.ProductId == productId && i.UserId == userId);
                item.Quantity = quantity;
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
                    .Where(x => x.UserId == userId).ToList();                

                return cartItems;
            }
        }
    }
}