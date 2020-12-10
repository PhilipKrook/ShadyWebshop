namespace LIAuppgift.Models.ViewModels
{
    using System.Collections.Generic;
    using LIAuppgift.Controllers;
    using Pages;

    public class CartPageViewModel
    {
        public CartPage CurrentPage { get; set; }
        public IEnumerable<ProductPage> Products { get; set; }
        public IEnumerable<CartPage> CartProducts { get; set; }
        public IEnumerable<CartItemEntity> CartItems { get; set; }
    }
}