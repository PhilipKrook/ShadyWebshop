namespace LIAuppgift.Controllers
{    
    using System.Web.Mvc;
    using EPiServer.Web.Mvc;
    using LIAuppgift.Business.Repositories;
    using Models.Pages;
    using Models.ViewModels;

    public class CartPageController : PageController<CartPage>
    {
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
    }   
}