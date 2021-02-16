using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIAuppgift.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
    }
}