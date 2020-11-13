using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer;

namespace LIAuppgift.Controllers
{
    using System.Web.Mvc;
    using EPiServer.Web.Mvc;
    using EPiServer.ServiceLocation;
    using Models.Pages;
    using Models.ViewModels;
    using EPiServer.Core;
    public class CartPageController : PageController<CartPage>
    {
        public ActionResult Index(CartPage currentPage)
        {
            return View("~/Views/CartPage/Index.cshtml", currentPage);
        }
    }
}