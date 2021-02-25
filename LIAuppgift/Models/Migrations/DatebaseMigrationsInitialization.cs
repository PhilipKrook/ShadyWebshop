using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using LIAuppgift.Business.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LIAuppgift.Models.Migrations
{
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
