namespace LIAuppgift.Models.ViewModels
{
    using System.Collections.Generic;
    using EPiServer.Core;
    using Pages;

    public class ProductPageViewModel
    {
        public ProductPage CurrentPage { get; set; }

        public string ConvertedPrice { get; set; }
    }
}