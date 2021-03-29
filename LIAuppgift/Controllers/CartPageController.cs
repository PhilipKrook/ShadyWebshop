﻿namespace LIAuppgift.Controllers
{    
    using System.Web.Mvc;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Mvc;
    using EPiServer.Web.Routing;
    using LIAuppgift.Business.Repositories;
    using Models.Pages;
    using Models.ViewModels;

    public class CartPageController : PageController<CartPage>
    {
        private readonly IUrlResolver urlResolver;
        public CartPageController()
        {
            this.urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();
        }

        public ActionResult Index(CartPage currentPage)
        {
            var cartViewModel = new CartPageViewModel();
            cartViewModel.CurrentPage = currentPage;

            var cartCookie = this.Request.Cookies.Get("cart");
            if (cartCookie == null || string.IsNullOrWhiteSpace(cartCookie.Value))
            {
                return View("~/Views/CartPage/Index.cshtml", cartViewModel);
            }

            var cartRepository = new CartRepository();
            var cartItems = cartRepository.Get(cartCookie.Value);
            cartViewModel.CartItems = cartItems;

            return View("~/Views/CartPage/Index.cshtml", cartViewModel);
        }

        [HttpPost]
        public ActionResult Remove(CartPage currentPage, int productId)
        {
            var cartRepository = new CartRepository();
            var cartCookie = this.Request.Cookies.Get("cart");
            cartRepository.Remove(productId, cartCookie.Value);

            return Redirect(this.urlResolver.GetUrl(currentPage.ContentLink));
        }
    }   
}