namespace LIAuppgift.Models.Migrations
{
    using EPiServer.Cms.Shell;
    using EPiServer.Data;
    using EPiServer.Framework;
    using LIAuppgift.Business.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;

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