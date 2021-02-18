using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.UI;
using System;
using System.Linq;

namespace LIAuppgift.modules
{
    [EPiServer.Framework.InitializableModule]
    [EPiServer.Framework.ModuleDependency(typeof(EPiServer.Cms.UI.AspNetIdentity.ApplicationSecurityEntityInitialization))]
    [EPiServer.Framework.ModuleDependency(typeof(EPiServerUIInitialization))]
    public class MyInitializationModule : IInitializableModule
    {
        public void ConfigureContainer(EPiServer.ServiceLocation.ServiceConfigurationContext context)
        {
            //Configure your providers
        }
        public void Initialize(EPiServer.Framework.Initialization.InitializationEngine context) { }
        public void Uninitialize(EPiServer.Framework.Initialization.InitializationEngine context) { }        
    }
}