using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using LIAuppgift.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIAuppgift.Business.References
{
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