using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;

namespace LIAuppgift.Controllers
{
    using Models.Pages;
    public class ProductPageController : PageController<ProductPage>
    {
        public ActionResult Index(ProductPage currentPage)
        {
            return View("~/Views/ProductPage/Index.cshtml", currentPage);
        }
    }
}