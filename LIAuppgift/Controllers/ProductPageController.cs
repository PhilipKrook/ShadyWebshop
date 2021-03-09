namespace LIAuppgift.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Mvc;
    using LIAuppgift.Business.Repositories;
    using LIAuppgift.Models.Entites;
    using LIAuppgift.Models.Pages;
    using LIAuppgift.Models.ViewModels;
    using LIAuppgift.Business.Api;   
    
    public class ProductPageController : PageController<ProductPage>
    {
        public ActionResult Index(ProductPage currentPage)
        {
            var currencyClient = new CurrencyClient();
            var convertedPrice = currencyClient.GetConvertedFromUsd(currentPage.ProductPrice);

            var viewModel = new ProductPageViewModel();
            viewModel.CurrentPage = currentPage;
            viewModel.ConvertedPrice = convertedPrice.ToString("C3");

            return View("~/Views/ProductPage/Index.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult Index(string productId)
        {
            var cartCookie = this.Request.Cookies.Get("cart");
            if (cartCookie == null || string.IsNullOrWhiteSpace(cartCookie.Value))
            {
                var guid = Guid.NewGuid();
                cartCookie = new HttpCookie("cart", guid.ToString());
                Response.Cookies.Add(cartCookie);
            }

            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var productPage = contentRepository.Get<ProductPage>(new ContentReference(int.Parse(productId)));

            var cartItem = new CartItemEntity();
            cartItem.ProductId = int.Parse(productId);
            cartItem.ProductName = productPage.Name;
            cartItem.ProductPrice = int.Parse(productPage.ProductPrice);
            cartItem.UserId = cartCookie.Value; 

            var cartRepository = new CartRepository();
            cartRepository.Add(cartItem);

            var currencyClient = new CurrencyClient();
            var convertedPrice = currencyClient.GetConvertedFromUsd(productPage.ProductPrice);

            var viewModel = new ProductPageViewModel();
            viewModel.CurrentPage = productPage;
            viewModel.ConvertedPrice = convertedPrice.ToString("C3");

            return View("~/Views/ProductPage/Index.cshtml", viewModel);
        }
    }
}