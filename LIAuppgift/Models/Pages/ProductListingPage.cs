using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace LIAuppgift.Models.Pages
{
    [ContentType(DisplayName = "All Products Page", GUID = "0b13e4f2-6d41-42dd-b91f-38f3ef2ae863", Description = "")]
    public class ProductListingPage : PageData
    {
        [CultureSpecific]
        [Display(
            Name = "Main body",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        public virtual XhtmlString MainBody { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Product type",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        public virtual string ProductType { get; set; }
    }
}