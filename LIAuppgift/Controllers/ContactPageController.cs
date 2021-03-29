namespace LIAuppgift.Controllers
{
    using System.Web.Mvc;
    using EPiServer.Web.Mvc;
    using LIAuppgift.Models.Pages;    

    public class ContactPageController : PageController<ContactPage>
    {
        public ActionResult Index(ContactPage currentPage)
        {
            return View(currentPage);
        }
    }
}