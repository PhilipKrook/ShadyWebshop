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

        // Method for getting the converted price and sending it to the product page (viewmodel)
        public ActionResult Index(ProductPage currentPage)
        {
            var currencyClient = new CurrencyClient();
            var convertedPrice = currencyClient.GetConvertedFromUsd(currentPage.ProductPrice);

            var viewModel = new ProductPageViewModel();
            viewModel.CurrentPage = currentPage;
            viewModel.ConvertedPrice = convertedPrice.ToString("C3");

            return View("~/Views/ProductPage/Index.cshtml", viewModel);
        }


        // Post method to add the cart to a cookie based on a guid
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

            // Creates a new Entity for the cart
            var cartItem = new CartItemEntity();
            cartItem.ProductId = int.Parse(productId);
            cartItem.ProductName = productPage.Name;
            cartItem.ProductPrice = int.Parse(productPage.ProductPrice);
            cartItem.UserId = cartCookie.Value;

            // Gets the converted price and adds it to the cart Entity
            var currencyClient = new CurrencyClient();
            var convertedPrice = currencyClient.GetConvertedFromUsd(productPage.ProductPrice);
            cartItem.ConvertedPrice = convertedPrice;            

            var cartRepository = new CartRepository();
            cartRepository.Add(cartItem);

            // Adds the converted price to the viewmodel
            var viewModel = new ProductPageViewModel();
            viewModel.CurrentPage = productPage;
            viewModel.ConvertedPrice = convertedPrice.ToString("C3");

            return View("~/Views/ProductPage/Index.cshtml", viewModel);
        }
    }
}