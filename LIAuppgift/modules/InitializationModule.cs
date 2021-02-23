namespace LIAuppgift.Modules
{
    using EPiServer.Framework;
    using EPiServer.UI;

    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Cms.UI.AspNetIdentity.ApplicationSecurityEntityInitialization))]
    [ModuleDependency(typeof(EPiServerUIInitialization))]
    public class MyInitializationModule : IInitializableModule
    {
        public void ConfigureContainer(EPiServer.ServiceLocation.ServiceConfigurationContext context)
        {
            // Configure your providers
        }

        public void Initialize(EPiServer.Framework.Initialization.InitializationEngine context) 
        {
        }

        public void Uninitialize(EPiServer.Framework.Initialization.InitializationEngine context) 
        {
        }        
    }
}