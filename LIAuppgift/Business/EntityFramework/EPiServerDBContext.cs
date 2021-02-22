using LIAuppgift.Models.Entites;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIAuppgift.Business.EntityFramework
{
    public class EPiServerDbContext : IdentityDbContext<CustomUser>
    {
        public EPiServerDbContext()
            : base("EPiServerDB", throwIfV1Schema: false)
        {
        }
        public static EPiServerDbContext Create()
        {
            return new EPiServerDbContext();
        }
    }
}