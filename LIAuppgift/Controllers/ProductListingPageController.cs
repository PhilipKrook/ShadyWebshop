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

    public class ProductListingPageController : PageController<ProductListingPage>
    {
        public ActionResult Index(ProductListingPage currentPage)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var products = contentLoader.GetChildren<ProductPage>(currentPage.ContentLink);

            var viewModel = new ProductListingPageViewModel();
            viewModel.CurrentPage = currentPage;
            viewModel.Products = products;

            return View("~/Views/ProductListingPage/Index.cshtml", viewModel);
        }
    }
}