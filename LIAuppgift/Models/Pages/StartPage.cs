namespace LIAuppgift.Models.Pages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    [ContentType(DisplayName = "Start Page", GUID = "75b6efc8-41a7-44a4-8287-ccfa042a5d47", Description = "")]
    public class StartPage : PageData
    {
        [CultureSpecific]
        [Display(
                     Name = "Heading",
                     Description = "The page heading",
                     GroupName = SystemTabNames.Content,
                     Order = 100)]
        public virtual String Heading { get; set; }

        [CultureSpecific]
        [Display(
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 200)]
        public virtual XhtmlString MainBody { get; set; }

        [CultureSpecific]
        [Display(
                    Name = "Content Area",
                    Description = "Content Area is the space where you can add blocks etc",
                    GroupName = SystemTabNames.Content,
                    Order = 300)]
        public virtual ContentArea ContentArea { get; set; }
    }
}