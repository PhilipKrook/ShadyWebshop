namespace LIAuppgift.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Mvc;
    using LIAuppgift.Models.Pages;
    
    public class ProductPageController : PageController<ProductPage>
    {
        public ActionResult Index(ProductPage currentPage)
            {            
            return View("~/Views/ProductPage/Index.cshtml", currentPage);
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
            cartItem.Name = productPage.Name;
            cartItem.Price = int.Parse(productPage.Price);
            cartItem.UserId = cartCookie.Value;

            var cartRepository = new CartRepository();
            cartRepository.Add(cartItem);

            return View("~/Views/ProductPage/Index.cshtml", productPage);
        }
    }
}