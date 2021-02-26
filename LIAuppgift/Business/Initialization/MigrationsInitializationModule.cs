namespace LIAuppgift.Business.Initialization
{
    using System.Data.Entity;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using LIAuppgift.Business.Configuration;
    using LIAuppgift.Business.EntityFramework;

    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class MigrationsInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CartContext, CartConfiguration>());
        }
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}