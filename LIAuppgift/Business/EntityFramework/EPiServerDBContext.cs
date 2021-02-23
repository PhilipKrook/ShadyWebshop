namespace LIAuppgift.Business.EntityFramework
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using LIAuppgift.Models.Entites;

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