using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace LIAuppgift.Models.Pages
{
    using EPiServer.Web;

    [ContentType(DisplayName = "Product Page", GUID = "2bbb5982-6606-4c62-9a18-181d08619c91", Description = "This page displays the product and info about it")]
    public class ProductPage : PageData
    {
        [CultureSpecific]
        [Display(
             Name = "Product title",
             Description = "",
             GroupName = SystemTabNames.Content,
             Order = 100)]
        public virtual string Heading { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Product description",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        public virtual XhtmlString MainBody { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Article number",
            Description = "The products unique ID",
            GroupName = SystemTabNames.Content,
            Order = 300)]
        public virtual string ArticleNumber { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Product Price",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 400)]
        public virtual string ProductPrice { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Content Area - add image",
            Description = "Add image",
            GroupName = SystemTabNames.Content,
            Order = 500)]
        public virtual ContentArea ContentArea { get; set; }

        [UIHint(UIHint.Image)]
        [CultureSpecific]
        [Display(
            Name = "Image",
            Description = "Add image",
            GroupName = SystemTabNames.Content,
            Order = 600)]
        public virtual ContentReference TestImage { get; set; }
    }
}