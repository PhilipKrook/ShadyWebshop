namespace LIAuppgift.Models.Pages
{
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using System.ComponentModel.DataAnnotations;

    [ContentType(DisplayName = "Contact Page", GUID = "05bef1de-209c-48af-bdf6-c6d37f1ae9d9", Description = "Contact us page")]
    public class ContactPage : PageData
    {
        [CultureSpecific]
        [Display(
             Name = "Heading",
             Description = "Page Heading",
             GroupName = SystemTabNames.Content,
             Order = 100)]
        public virtual string Heading { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Content",
            Description = "Add the page's content here",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        public virtual XhtmlString MainBody { get; set; }
    }
}