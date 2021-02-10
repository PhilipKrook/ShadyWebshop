namespace LIAuppgift.Controllers
{
    using System.Web.Mvc;
    using EPiServer.Web.Mvc;
    using Models.Pages;

    public class StartPageController : PageController<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            return View("~/Views/StartPage/Index.cshtml", currentPage);
        }
    }
}
