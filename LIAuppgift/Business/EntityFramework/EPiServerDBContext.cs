namespace LIAuppgift.Business.EntityFramework
{
    using LIAuppgift.Models.Entities;

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