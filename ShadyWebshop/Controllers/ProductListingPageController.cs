namespace LIAuppgift.Controllers
{
    using System.Web.Mvc;
    using EPiServer;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Mvc;
    using Models.Pages;
    using Models.ViewModels;

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