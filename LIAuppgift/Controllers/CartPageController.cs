namespace LIAuppgift.Controllers
{
    using System.Linq;
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

        // Sets up a new cart
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

        // Remove product from cart
        [HttpPost]
        public ActionResult Remove(CartPage currentPage, int productId)
        {
            var cartRepository = new CartRepository();
            var cartCookie = this.Request.Cookies.Get("cart");
            cartRepository.Remove(productId, cartCookie.Value);

            return Redirect(this.urlResolver.GetUrl(currentPage.ContentLink));
        }

        // Update product quantity in cart
        [HttpPost]
        public ActionResult Update(CartPage currentPage, int productId, int quantity)
        {
            var cartRepository = new CartRepository();
            var cartCookie = this.Request.Cookies.Get("cart");
            cartRepository.Update(productId, quantity, cartCookie.Value);

            var cartItems = cartRepository.Get(cartCookie.Value);
            var cartViewModel = new CartPageViewModel();
            cartViewModel.CurrentPage = currentPage;
            cartViewModel.CartItems = cartItems;

            return View("~/Views/CartPage/Index.cshtml", cartViewModel);
        }





        /*public double CartSum()
        {
            var cartRepository = new CartRepository();
            var cartCookie = this.Request.Cookies.Get("cart");
            var cartItems = cartRepository.Get(cartCookie.Value);
           
            double total = 0;
            foreach (var cartItem in cartItems)
            {
                total += cartItem.Quantity * cartItem.ConvertedPrice;
            }

            var Total = total;



            var cartRepository = new CartRepository();
            var cartCookie = this.Request.Cookies.Get("cart");
            var cartItems = cartRepository.Get(cartCookie.Value);


            double total = cartItems.Sum(x => x.Quantity * x.ConvertedPrice);
            var Total = total;


            var total = Model.CartItems.Where(x => x.UserId == UserId)
                .Select(x => x.Quantity * x.ConvertedPrice).Sum();

            return Total;
        }*/




    }
}