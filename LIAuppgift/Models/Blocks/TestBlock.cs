namespace LIAuppgift.Models.Blocks
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    [ContentType(DisplayName = "TestBlock",
                 GUID = "38d57768-e09e-4da9-90df-54c73c61b270",
                 Description = "Testing block")]
    public class TestBlock : BlockData
    {
        [CultureSpecific]
        [Display(Name = "Block heading",
                 Description = "Add a heading.",
                 GroupName = SystemTabNames.Content,
                 Order = 100)]
        public virtual String Heading { get; set; }

        [Display(Name = "Image", Description = "Add an image (optional)",
                 GroupName = SystemTabNames.Content,
                 Order = 200)]
        public virtual ContentReference Image { get; set; }
    }
}