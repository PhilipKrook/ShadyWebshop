namespace LIAuppgift.Models.ViewModels
{
    using System.Collections.Generic;
    using LIAuppgift.Models.Entites;
    using Pages;

    public class CartPageViewModel
    {
        public CartPageViewModel()
        {
            this.CartItems = new List<CartItemEntity>();
        }

        public CartPage CurrentPage { get; set; }

        public double Subtotal { get; set; }

        public IEnumerable<ProductPage> Products { get; set; }

        public IEnumerable<CartPage> CartProducts { get; set; }

        public IEnumerable<CartItemEntity> CartItems { get; set; }

    }
}