namespace LIAuppgift.Models.Media
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Framework.DataAnnotations;

    [ContentType(DisplayName = "GenericMedia", GUID = "6ed19855-8760-4bfa-991f-0454f81b5096", Description = "Used for generic file types such as Word or PDF documents.")]
    [MediaDescriptor(ExtensionString = "pdf,doc,docx")]
    public class GenericMedia : MediaData
    {
        [CultureSpecific]
        [Editable(true)]
        [Display(
            Name = "Media description",
            Description = "Add a description of the content.",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        public virtual String Description { get; set; }
    }
}