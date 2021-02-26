namespace LIAuppgift.Models.Migrations
{
    using EPiServer.Cms.Shell;
    using EPiServer.Framework;    

    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class MigrateDatabaseToLatestVersionInitialization : InitializableModule
    {
       /* protected override void OnInitialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EPiServerDbContext, EPiServerDbContextConfiguration>());
        }

        protected override void OnUninitialize()
        {
            throw new NotImplementedException();
        }*/
    }
}