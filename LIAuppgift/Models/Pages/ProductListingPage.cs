using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace LIAuppgift.Models.Pages
{
    using EPiServer.Web;

    [ContentType(DisplayName = "All Products Page", GUID = "0b13e4f2-6d41-42dd-b91f-38f3ef2ae863", Description = "Lists all products")]
    public class ProductListingPage : PageData
    {
        [UIHint(UIHint.Image)]
        [CultureSpecific]
        [Display(
            Name = "Image",
            Description = "Add Image",
            GroupName = SystemTabNames.Content,
            Order = 500)]
        public virtual ContentReference ProductImage { get; set; }
    }
}