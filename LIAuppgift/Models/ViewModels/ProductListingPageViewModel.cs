using System;
using System.Linq;
using System.Web;
using EPiServer.Core;

namespace LIAuppgift.Models.ViewModels
{
    using System.Collections.Generic;
    using Pages;
    public class ProductListingPageViewModel
    {
        public ProductListingPage CurrentPage { get; set; }
        public IEnumerable<ProductPage> Products { get; set; }
    }
}