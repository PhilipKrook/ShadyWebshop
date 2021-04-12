namespace LIAuppgift.Models.Entites
{
    using LIAuppgift.Business.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class CartItemEntity
    {
        private readonly CartContext _cartContext;

        private CartItemEntity(CartContext cartContext)
        {
            _cartContext = cartContext;
        }

        public double GetCartTotal()
        {
            var total = _cartContext.CartItems.Where(c => c.UserId == UserId)
                .Select(c => c.ConvertedPrice * c.Quantity).Sum();
            return total;
        }

        [Key]
        public int Id { get; set; }

        public int ProductPrice { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

/*        public double SumPrice { get; set; }
*/
        public double ConvertedPrice { get; set; }

        public string ProductName { get; set; }

        public string UserId { get; set; }
    }
}