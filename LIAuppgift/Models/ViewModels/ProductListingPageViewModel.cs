namespace LIAuppgift.Models.ViewModels
{
    using System.Collections.Generic;
    using EPiServer.Core;
    using Pages;

    public class ProductListingPageViewModel
    {
        public ProductListingPage CurrentPage { get; set; }
        public IEnumerable<ProductPage> Products { get; set; }
        public virtual ContentReference ProductImage { get; set; }
        public IEnumerable<ProductPage> CartProducts { get; internal set; }
    }
}