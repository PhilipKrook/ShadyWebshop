using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;


namespace LIAuppgift.Models.Media
{
    [ContentType(DisplayName = "ImageFile", GUID = "207cd5da-8054-4710-a352-1859a68d6a57", Description = "Used for images of different file formats.")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png")]
    public class ImageFile : ImageData
    {        
                public virtual string Copyright { get; set; }
                public virtual String Description { get; set; }         
    }
}