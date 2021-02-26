﻿namespace LIAuppgift.Controllers
{
    using System.Data.Entity;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;

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