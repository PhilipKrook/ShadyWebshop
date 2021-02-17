using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Shell.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIAuppgift.Models.Entities
{
    public class CustomUser : ApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}