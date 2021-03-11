using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using LIAuppgift.Models.Pages;

namespace LIAuppgift.Business.References
{
    // Using these references we are able to "link" between pages with content
    public class StartPageReferences
    {
        public static ContentReference CartReference()
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();            
            var startpage = contentLoader.Get<StartPage>(ContentReference.StartPage);

            return startpage.CartPageReference;
        }

        public static Url LoginReference()
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var startpage = contentLoader.Get<StartPage>(ContentReference.StartPage);

            return startpage.LoginPageReference;
        }
    }
}