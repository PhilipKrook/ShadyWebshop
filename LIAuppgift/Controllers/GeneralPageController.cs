namespace LIAuppgift.Controllers
{
    using System.Web.Mvc;
    using EPiServer.Web.Mvc;
    using LIAuppgift.Models.Pages;    

    public class GeneralPageController : PageController<GeneralPage>
    {
        public ActionResult Index(GeneralPage currentPage)
        {
            return View(currentPage);
        }
    }
}