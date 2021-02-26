namespace LIAuppgift.Business.Initialization
{
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using LIAuppgift.Business.Configuration;
    using LIAuppgift.Business.EntityFramework;
    using System.Data.Entity;

    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DatabaseMigrationsInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EPiServerDbContext, EPiServerDbContextConfiguration>());
        }
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
